using AlayamMgmt.Web.Models;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web;
using System.Web.Http;




using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;

using AlayamMgmt.Web.Controllers;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.Owin;

using Microsoft.AspNet.Identity;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Web.Mapper;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseApiController
    {

        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody] UserModel model)
        {
            HttpResponseMessage response;

            try
            {
                UserDBEntity userDBEntity = GetUserDBEntity(model, true);               

                IUserBusiness userBusinessbusiness = new UserBusiness(model.UserName);
                userBusinessbusiness.Save(userDBEntity);

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Save");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (DbUpdateException ex)
            {
                if (null != ex.InnerException || ex.InnerException.InnerException != null)
                {
                    var innerException = ex.InnerException.InnerException
                                               as System.Data.SqlClient.SqlException;
                    if (innerException != null &&
                           (
                               innerException.Number == 2627 ||
                               innerException.Number == 2601)
                           )
                    {
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.UserName), this.GetType(), "Save");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Edit")]
        [HttpPost]
        public HttpResponseMessage Edit([FromBody] UserModel model)
        {
            HttpResponseMessage response;
            try
            {
                UserDBEntity userDBEntity = GetUserDBEntity(model, false);

                IUserBusiness userBusinessbusiness = new UserBusiness(model.UserName);
                userBusinessbusiness.EditUser(userDBEntity);

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Edit");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Edit");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (DbUpdateException ex)
            {
                if (null != ex.InnerException || ex.InnerException.InnerException != null)
                {
                    var innerException = ex.InnerException.InnerException
                                               as System.Data.SqlClient.SqlException;
                    if (innerException != null &&
                           (
                               innerException.Number == 2627 ||
                               innerException.Number == 2601)
                           )
                    {
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.UserName), this.GetType(), "Edit");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Edit");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Edit");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Edit");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            IUserBusiness userBusinessbusiness = new UserBusiness(Constant.DEFAULT_EMAIL);
            List<UserModel> userModel = DBEntityToModel.GetUserModels(userBusinessbusiness.Get(active));
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, userModel);
            return response;  
        }


        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetByOrgId(int OrgId, bool? active)
        {
            IUserBusiness userBusinessbusiness = new UserBusiness(Constant.DEFAULT_EMAIL);
            List<UserModel> userModel = DBEntityToModel.GetUserModels(userBusinessbusiness.GetByOrgId(OrgId, active));
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, userModel);
            return response;  
        }      


        [CustomAuthorizeAttribute]
        [Route("IsUnique")]
        [HttpGet]
        public HttpResponseMessage IsUnique(int id, string name)
        {
            HttpResponseMessage response;
            try
            {
                IUserBusiness userBusinessbusiness = new UserBusiness(Constant.DEFAULT_EMAIL);
                bool returnValue = userBusinessbusiness.IsUnique(id, name);

                response = Request.CreateResponse(HttpStatusCode.OK, returnValue);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "IsUnique");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }
        
        [Route("ForgotPassword")]
        [HttpPost]
        public HttpResponseMessage ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            HttpResponseMessage response;
            try
            {
                IUserBusiness userBusinessbusiness = new UserBusiness(Constant.DEFAULT_EMAIL);
                string password = userBusinessbusiness.ForgotPassword(forgotPasswordModel.Email);

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "ForgotPassword");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "ForgotPassword");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "ForgotPassword");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Enable")]
        [HttpPost]
        public HttpResponseMessage Enable([FromBody] EnableModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                IUserBusiness userBusinessbusiness = new UserBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = userBusinessbusiness.Enable(enableModel.Id, enableModel.Active);

                response = Request.CreateResponse(HttpStatusCode.OK, returnValue);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Enable");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Enable");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Enable");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        public static UserDBEntity GetUserDBEntity(UserModel user, bool newUser)
        {
            if (newUser == true)
            {
                string encryptionKey = ConfigurationManager.AppSettings["encryptionKey"].ToString();
                IHasher hasher = new HMACSHA512Hasher(encryptionKey);
                user.Password = hasher.Hash(user.Password);
            }

            UserDBEntity userDBEntity = new UserDBEntity()
            {
                Id = user.Id,
                CreateDateTime = user.CreateDateTime,
                CreateUser = user.CreateUser,
                LastUpdateDateTime = user.LastUpdateDateTime,
                LastUpdateUser = user.LastUpdateUser,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Active = user.Active,
                PasswordHash = user.Password
            };

            userDBEntity.UserRoles = new List<UserRoleDBEntity>();
            userDBEntity.UserRoles.Add(new UserRoleDBEntity() { RoleId = user.RoleId, OrganizationId = user.OrganizationId });
            return userDBEntity;

        }

    }
}
