using AlayamMgmt.Business;
using AlayamMgmt.BusinessLogic;
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
    [RoutePrefix("api/Deposit")]
    public class DepositController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<DepositDBEntity> depositBusiness = new BaseBusiness<DepositDBEntity>(Constant.DEFAULT_EMAIL);
                List<DepositModel> depositBusinessModels = GetDepositModels(depositBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, depositBusinessModels);
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
                IDepositBusiness depositBusiness = new DepositBusiness(Constant.DEFAULT_EMAIL);
                DepositModel depositModel = GetDepositModel(depositBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, depositModel);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        public HttpResponseMessage GetByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IDepositBusiness depositBusiness = new DepositBusiness(Constant.DEFAULT_EMAIL);
                List<DepositModel> depositBusinessModels = GetDepositModels(depositBusiness.GetOrgId(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, depositBusinessModels);
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
        [Route("GetRecentDepositsByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetRecentDepositsByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                IDepositBusiness depositBusiness = new DepositBusiness(Constant.DEFAULT_EMAIL);

                List<DepositModel> depositBusinessModels = GetDepositModels(depositBusiness.GetRecentDepositsByOrgId(orgId));

                response = Request.CreateResponse(HttpStatusCode.OK, depositBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentMembers");
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
                IDepositBusiness depositBusiness = new DepositBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = depositBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(DepositModel model)
        {
            HttpResponseMessage response;

            try
            {
                IDepositBusiness DepositBusiness = new DepositBusiness(Constant.DEFAULT_EMAIL);                
                DepositBusiness.Save(GetDepositDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, "Deposit"), this.GetType(), "Save");
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

        //[CustomAuthorizeAttribute]
        [Route("Report")]
        public HttpResponseMessage Report(DepositReportRequestModel report)
        {
            HttpResponseMessage response;

            try
            {
                IDepositBusiness depositBusiness = new DepositBusiness(Constant.DEFAULT_EMAIL);
                List<DepositModel> depositModels = GetDepositModels(depositBusiness.Report(report.OrganizationId, report.DepositStartDate, report.DepositEndDate, report.AccountId, report.UserId, report.OrderById));

                response = Request.CreateResponse(HttpStatusCode.OK, depositModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByBranchId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        private List<DepositModel> GetDepositModels(List<DepositDBEntity> entities)
        {
            List<DepositModel> models = new List<DepositModel>();

            entities.ForEach(x => models.Add(GetDepositModel(x)));

            return models;
        }

        private DepositDBEntity GetDepositDBEntity(DepositModel model)
        {
            return new DepositDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                Description = model.Description,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,                
                AccountId = model.AccountId,
                RowTimeStamp = model.RowTimeStamp,
                DepositDate = model.DepositDate,                
                Amount = model.Amount,
                OfferingDate = model.OfferingDate,
                UserId = model.UserId
            };
        }

        private DepositModel GetDepositModel(DepositDBEntity entity)
        {
            return new DepositModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                Description = entity.Description,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                AccountId = entity.AccountId,
                Account = entity.Account.Name,
                RowTimeStamp = entity.RowTimeStamp,
                DepositDate = entity.DepositDate,
                Amount = entity.Amount,
                OfferingDate = entity.OfferingDate,
                UserId = entity.UserId,
                User = entity.User.FirstName + " " + entity.User.LastName,
                Bank = entity.Account.Bank.Name
            };
        }

    }
}
