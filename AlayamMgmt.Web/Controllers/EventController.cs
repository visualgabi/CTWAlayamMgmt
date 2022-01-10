using AlayamMgmt.Business;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Models;
using AlayamMgmt.Web.Providers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/Event")]
    public class EventController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<EventDBEntity> eventBusiness = new BaseBusiness<EventDBEntity>(Constant.DEFAULT_EMAIL);
                List<EventModel> eventBusinessModels = GetEventModels(eventBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, eventBusinessModels);
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
        //[CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        public HttpResponseMessage GetByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IEventBusiness eventBusiness = new EventBusiness(Constant.DEFAULT_EMAIL);
                List<EventModel> eventBusinessModels = GetEventModels(eventBusiness.GetOrgId(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, eventBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByBranchId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetRecentEventsByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetRecentEventsByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                IEventBusiness eventBusiness = new EventBusiness(Constant.DEFAULT_EMAIL);
                List<EventModel> eventBusinessModels = GetEventModels(eventBusiness.GetRecentEventsByOrgId(orgId));

                response = Request.CreateResponse(HttpStatusCode.OK, eventBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentEventsByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        //[CustomAuthorizeAttribute]
        [Route("Report")]
        public HttpResponseMessage Report(EventReportRequestModel report)
        {
            HttpResponseMessage response;

            try
            {
                IEventBusiness eventBusiness = new EventBusiness(Constant.DEFAULT_EMAIL);
                bool? splEventType = null;

                if (report.SplEventTypeId != null && report.SplEventTypeId == 1)
                    splEventType = false;
                if (report.SplEventTypeId != null && report.SplEventTypeId == 2)
                    splEventType = true;


                List<EventModel> eventBusinessModels = GetEventModels(eventBusiness.Report(report.OrganizationId, report.EventStartDate, report.EventEndDate, report.EventTypeId, splEventType, report.OrderById));

                response = Request.CreateResponse(HttpStatusCode.OK, eventBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByBranchId");
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
                IEventBusiness eventBusiness = new EventBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = eventBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(EventModel model)
        {
            HttpResponseMessage response;

            try
            {
                IEventBusiness EventBusiness = new EventBusiness(Constant.DEFAULT_EMAIL);
                EventBusiness.Save(GetEventDBEntity(model));

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

        private List<EventModel> GetEventModels(List<EventDBEntity> entities)
        {
            List<EventModel> models = new List<EventModel>();

            entities.ForEach(x => models.Add(GetEventModel(x)));

            return models;
        }
        
        private EventDBEntity GetEventDBEntity(EventModel model)
        {
            return new EventDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                Description = model.Description,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                Name = model.Name,
                BranchId = model.BranchId,
                RowTimeStamp = model.RowTimeStamp,
                EventDate = model.EventDate,
                EventTypeId = model.EventTypeId,
                Expense = model.Expense,
                IsSpecialEvent = model.IsSpecialEvent,
                NoOfAdultAttended = model.NoOfAdultAttended,
                NoOfKidsParticipated = model.NoOfKidsParticipated,
                NoOfMenAttended = model.NoOfMenAttended,
                NoOfWomenAttended = model.NoOfWomenAttended,
                Offering = model.Offering,
                SplGuestDetails = model.SplGuestDetails
            };
        }

        private EventModel GetEventModel(EventDBEntity entity)
        {
            return new EventModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                Description = entity.Description,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                Name = entity.Name,
                BranchId = entity.BranchId,
                RowTimeStamp = entity.RowTimeStamp,
                EventDate = entity.EventDate,
                EventTypeId = entity.EventTypeId,
                Expense = entity.Expense,
                IsSpecialEvent = entity.IsSpecialEvent,
                NoOfAdultAttended = entity.NoOfAdultAttended,
                NoOfKidsParticipated = entity.NoOfKidsParticipated,
                NoOfMenAttended = entity.NoOfMenAttended,
                NoOfWomenAttended = entity.NoOfWomenAttended,
                Offering = entity.Offering,
                SplGuestDetails = entity.SplGuestDetails,
                EventType = entity.EventType.Name,
                Branch = entity.Branch.Name
            };
        }

    }
}
