using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Controllers;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlayamMgmt.Common.DBEntity;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/SMTPSetting")]
    public class SMTPSettingController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgSMTPDetailsBusiness orgSMTPDetailsBusiness = new SMTPSettingBusiness(claimModel.LoginUserEmail);
                List<SMTPSettingModel> orgSMTPDetailsModels = GetOrgSMTPDetailsModels(orgSMTPDetailsBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, orgSMTPDetailsModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
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
                ClaimModel claimModel = GetClaimModel();
                IOrgSMTPDetailsBusiness orgSMTPDetailsBusiness = new SMTPSettingBusiness(claimModel.LoginUserEmail);
                byte[] returnValue = orgSMTPDetailsBusiness.Enable(enableModel.Id, enableModel.Active);

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

        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]SMTPSettingModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();
                IOrgSMTPDetailsBusiness orgSMTPDetailsBusiness = new SMTPSettingBusiness(claimModel.LoginUserEmail);
                orgSMTPDetailsBusiness.Save(GetOrgSMTPDetailsDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, "Offering"), this.GetType(), "Save");
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

        private SMTPSettingDBEntity GetOrgSMTPDetailsDBEntity(SMTPSettingModel model)
        {
            return new SMTPSettingDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                FromEmailId = model.FromEmailId,
                FromEmailLabel = model.FromEmailLabel,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                OrganizationId = model.OrganizationId,
                RowTimeStamp = model.RowTimeStamp,
                SMTPServer = model.SMTPServer,
                SMTPServerPassword = model.SMTPServerPassword,
                SMTPServerPort = model.SMTPServerPort,
                SMTPServerUserName = model.SMTPServerUserName
            };
        }

        private List<SMTPSettingModel> GetOrgSMTPDetailsModels(List<SMTPSettingDBEntity> entities)
        {
            List<SMTPSettingModel> models = new List<SMTPSettingModel>();

            entities.ForEach(x => models.Add(GetOrgSMTPDetailsModel(x)) );

            return models;
        }

        private SMTPSettingModel GetOrgSMTPDetailsModel(SMTPSettingDBEntity entity)
        {
            return new SMTPSettingModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                FromEmailId = entity.FromEmailId,
                FromEmailLabel = entity.FromEmailLabel,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                OrganizationId = entity.OrganizationId,
                RowTimeStamp = entity.RowTimeStamp,
                SMTPServer = entity.SMTPServer,
                SMTPServerPassword = entity.SMTPServerPassword,
                SMTPServerPort = entity.SMTPServerPort,
                SMTPServerUserName = entity.SMTPServerUserName,
                Organization = entity.Organization.Name
            };
        }
    }
}
