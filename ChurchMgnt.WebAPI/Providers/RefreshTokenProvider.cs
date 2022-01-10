using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChurchMgnt.WebAPI.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {

        RefreshTokenDBEntity entity = null;

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];
            IRefreshTokenBusiness refreshTokenBusiness = new RefreshTokenBusiness(Constant.DEFAULT_EMAIL);

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            if (entity == null)
            {
                var refreshTokenId = Guid.NewGuid().ToString("n");
                entity = new RefreshTokenDBEntity()
                {
                    TokenId = HashHelper.GetHash(refreshTokenId),
                    ClientId = clientid,
                    Email = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime)),
                    Active = true
                };

            }



            context.Ticket.Properties.IssuedUtc = entity.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = entity.ExpiresUtc;

            entity.ProtectedTicket = context.SerializeTicket();

            if (entity.Id == 0)
                refreshTokenBusiness.Save(entity);

            context.SetToken(entity.TokenId);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            //string hashedTokenId = HashHelper.GetHash(context.Token);
            string hashedTokenId = context.Token;

            IRefreshTokenBusiness refreshTokenBusiness = new RefreshTokenBusiness(Constant.DEFAULT_EMAIL);

            entity = refreshTokenBusiness.GetByTokenId(hashedTokenId);
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            if (entity != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(entity.ProtectedTicket);
                entity.Active = true;
                entity.IssuedUtc = DateTime.UtcNow;
                entity.ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime));
                refreshTokenBusiness.Save(entity);
            }

        }
    }
}