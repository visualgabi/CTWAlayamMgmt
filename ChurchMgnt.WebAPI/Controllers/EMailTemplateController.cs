using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/EMailTemplate")]
    public class EMailTemplateController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IEMailTemplateBusiness EMailTemplateBusiness = new EMailTemplateBusiness(Constant.DEFAULT_EMAIL);
                List<EMailTemplateModel> BankBusinessModels = GetEMailTemplateModels(EMailTemplateBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, BankBusinessModels);
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
        [Route("GetById")]
        public HttpResponseMessage GetById(int id)
        {
            HttpResponseMessage response;

            try
            {
                IEMailTemplateBusiness EMailTemplateBusiness = new EMailTemplateBusiness(Constant.DEFAULT_EMAIL);
                EMailTemplateModel model = GetEMailTemplateModel(EMailTemplateBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, model);
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
        [Route("GetByOrgId")]
        public HttpResponseMessage GetByOrgId(int OrgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IEMailTemplateBusiness EMailTemplateBusiness = new EMailTemplateBusiness(Constant.DEFAULT_EMAIL);
                List<EMailTemplateModel> BankBusinessModels = GetEMailTemplateModels(EMailTemplateBusiness.GetOrgId(OrgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, BankBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetOrgId");
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
                IEMailTemplateBusiness EMailTemplateBusiness = new EMailTemplateBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = EMailTemplateBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(EMailTemplateModel model)
        {
            HttpResponseMessage response;

            try
            {
                IEMailTemplateBusiness EMailTemplateBusiness = new EMailTemplateBusiness(Constant.DEFAULT_EMAIL);
                EMailTemplateBusiness.Save(GetEMailTemplateDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.Name), this.GetType(), "Save");
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

        protected List<EMailTemplateModel> GetEMailTemplateModels(List<EMailTemplateDBEntity> entities)
        {
            List<EMailTemplateModel> models = new List<EMailTemplateModel>();
            entities.ForEach(x => models.Add(GetEMailTemplateModel(x)));

            return models;
        }

        protected EMailTemplateModel GetEMailTemplateModel(EMailTemplateDBEntity entity)
        {
            return new EMailTemplateModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                Description = entity.Description,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                Name = entity.Name,
                OrganizationId = entity.OrganizationId,
                RowTimeStamp = entity.RowTimeStamp,
                Content = entity.Content
            };
        }

        protected EMailTemplateDBEntity GetEMailTemplateDBEntity(EMailTemplateModel model)
        {
            return new EMailTemplateDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                Description = model.Description,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                Name = model.Name,
                OrganizationId = model.OrganizationId,
                RowTimeStamp = model.RowTimeStamp,
                Content = model.Content
            };
        }
    }
}
