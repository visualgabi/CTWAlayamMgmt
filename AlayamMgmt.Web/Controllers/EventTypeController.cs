using AlayamMgmt.Business;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/EventType")]
    public class EventTypeController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IEventTypeBusiness eventTypeBusiness = new EventTypeBusiness(Constant.DEFAULT_EMAIL);
                List<OrganizationLookupModel> EventTypeBusinessModels = GetOrganizationLookModels<EventTypeDBEntity>(eventTypeBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, EventTypeBusinessModels);
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
                IEventTypeBusiness eventTypeBusiness = new EventTypeBusiness(Constant.DEFAULT_EMAIL);
                List<OrganizationLookupModel> EventTypeBusinessModels = GetOrganizationLookModels<EventTypeDBEntity>(eventTypeBusiness.GetOrgId(OrgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, EventTypeBusinessModels);
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
                IEventTypeBusiness eventTypeBusiness = new EventTypeBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = eventTypeBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(OrganizationLookupModel model)
        {
            HttpResponseMessage response;

            try
            {
                IEventTypeBusiness eventTypeBusiness = new EventTypeBusiness(Constant.DEFAULT_EMAIL);
                eventTypeBusiness.Save(GetEventTypeDBEntity(model));

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

        [CustomAuthorizeAttribute]
        [Route("IsUnique")]
        [HttpGet]
        public HttpResponseMessage IsUnique(int id, int orgId, string name)
        {
            HttpResponseMessage response;
            try
            {
                IEventTypeBusiness eventTypeBusiness = new EventTypeBusiness(Constant.DEFAULT_EMAIL);
                bool returnValue = eventTypeBusiness.IsUnique(id, orgId, name);

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

        protected EventTypeDBEntity GetEventTypeDBEntity(OrganizationLookupModel model)
        {
            return new EventTypeDBEntity()
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
                RowTimeStamp = model.RowTimeStamp
            };
        } 
    }
}
