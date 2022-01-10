using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;

using ChurchMgnt.WebAPI.Controllers;
//using ChurchMgnt.Web.App_Start;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using ChurchMgnt.WebAPI.Models;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Mapper;
using Microsoft.Owin;
using System.Text;
using System.Configuration;


namespace ChurchMgnt.WebAPI.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            ClientDBEntity client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            client = new ClientBusiness(Constant.DEFAULT_EMAIL).getClientByName(context.ClientId);

           //ClientDBEntity clientEntity = new AuthBusiness().FindClient(context.ClientId);

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != HashHelper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
               // var userManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
               
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
                                
                IUserBusiness userBusniess = new UserBusiness(context.UserName);

              

                UserModel userModel = DBEntityToModel.GetUserModel(userBusniess.ValidateUser(context.UserName, context.Password));

                if (userModel == null)
                {                   
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }               

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, userModel.RoleId.ToString() ) );
                identity.AddClaim(new Claim("userId", userModel.Id.ToString()));
                identity.AddClaim(new Claim("OrgId", userModel.OrganizationId == null ? string.Empty : userModel.OrganizationId.ToString()) );
                identity.AddClaim(new Claim("orgName", userModel.OrganizationId == null ? string.Empty : userModel.Organization));
                identity.AddClaim(new Claim("sub", context.UserName));

                ITaxFilingBusiness taxFilingBusiness = new TaxFilingBusiness(context.UserName);
                IBirthdayRemainderBusiness birthdayRemainderBusiness = new BirthdayRemainderBusiness(context.UserName);


                DateTime lastTaxFiledFiscalEndDate = DateTime.Now.AddYears(-5);
                bool birthdayAvailable = false;

                if (userModel.OrganizationId != null)
                {
                    lastTaxFiledFiscalEndDate = taxFilingBusiness.GetLastTaxFiledFiscalYearEndDateByOrgId(userModel.OrganizationId.Value);
                    //birthdayAvailable = birthdayRemainderBusiness.IsAnybodyBirthdayToday(userModel.OrganizationId.Value) > 0;

                    //if(birthdayAvailable == false)
                    //{
                    //    IMarriageAnniversaryRemainderBusiness marriageAnniversaryRemainderBusiness = new MarriageAnniversaryRemainderBusiness(context.UserName);
                    //    birthdayAvailable = marriageAnniversaryRemainderBusiness.IsAnybodyMarriageAnniversarydayToday(userModel.OrganizationId.Value) > 0;
                    //}
                }

                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                         "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", context.UserName
                    },
                    { 

                        "role", userModel.RoleId.ToString()                        
                    },
                    { 
                        "orgId", userModel.OrganizationId == null ? string.Empty : userModel.OrganizationId.ToString()
                    },                    
                    { 
                        "orgName", userModel.OrganizationId == null ? string.Empty : userModel.Organization
                    },                                      
                    { 
                        "userId", userModel.Id.ToString()
                    },                    
                    { 
                        "currency", userModel.OrganizationId == null ? string.Empty : userModel.Currency
                    },                    
                    { 
                        "appVersion", ConfigurationManager.AppSettings["appVersion"].ToString()
                    },
                    { 
                        "lastTaxFiledFiscalEndDate", lastTaxFiledFiscalEndDate.ToShortDateString()
                    },
                    {
                        "birthdayAvailable", birthdayAvailable.ToString()
                    }
                    
                });

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            catch(Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GrantResourceOwnerCredentials");
                context.SetError("invalid_grant", "Unhandled exception occured, Please try it later");
                return;
            }

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }       
    }

    //public class AuthenticationMiddleware : OwinMiddleware
    //{
    //    OwinMiddleware _next;
    //    public AuthenticationMiddleware(OwinMiddleware next) :
    //        base(next) {
    //            _next = next;
    //    }

    //    public override async Task Invoke(IOwinContext context)
    //    {
    //        var response = context.Response;

    //        string[] str = { "sample" };

    //        //response.OnSendingHeaders(state =>
    //        //{
    //        //    var resp = (OwinResponse)state;
    //        //    resp.Headers.Add("X-MyResponse-Header", str);
    //        //    resp.StatusCode = 403;
    //        //    resp.ReasonPhrase = "Forbidden"; // if you're going to change the status code
    //        //    // you probably should also change the reason phrase
    //        //}, response);

            

    //        await _next.Invoke(context);
    //    }
    //}
}