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
    [RoutePrefix("api/OpeningBalance")]
    public class OpeningBalanceController : ApiController
    {       

        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        public HttpResponseMessage GetByOrgId(int OrgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IOpeningBalanceBusiness openingBalanceBusiness = new OpeningBalanceBusiness(Constant.DEFAULT_EMAIL);
                List<OpeningBalanceModel> expenseTypeBusinessModels = toOpeningBalanceModels(openingBalanceBusiness.GetByOrgId(OrgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, expenseTypeBusinessModels);
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
                IOpeningBalanceBusiness openingBalanceBusiness = new OpeningBalanceBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = openingBalanceBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(OpeningBalanceModel model)
        {
            HttpResponseMessage response;

            try
            {
                IOpeningBalanceBusiness openingBalanceBusiness = new OpeningBalanceBusiness(Constant.DEFAULT_EMAIL);
                openingBalanceBusiness.Save(toOpeningBalanceDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, "Opening Balance"), this.GetType(), "Save");
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


        private OpeningBalanceDBEntity toOpeningBalanceDBEntity(OpeningBalanceModel model)
        {
            return new OpeningBalanceDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,                
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,  
                Amount = model.Amount,
                AccountId = model.AccountId,
                OrgFiscalYearId = model.OrgFiscalYearId,
                RowTimeStamp = model.RowTimeStamp
            };

        }


        private List<OpeningBalanceModel> toOpeningBalanceModels(List<OpeningBalanceDBEntity> entities)
        {
            List<OpeningBalanceModel> models = new List<OpeningBalanceModel>();

            foreach (OpeningBalanceDBEntity entity in entities)
            {
                models.Add(new OpeningBalanceModel()
                {
                    Active = entity.Active,
                    CreateDateTime = entity.CreateDateTime,
                    CreateUser = entity.CreateUser,                    
                    Id = entity.Id,
                    LastUpdateDateTime = entity.LastUpdateDateTime,
                    LastUpdateUser = entity.LastUpdateUser,
                    Amount = entity.Amount,
                    AccountId = entity.AccountId,
                    OrgFiscalYearId = entity.OrgFiscalYearId,
                    Account = entity.Account.Name,
                    OrgFiscalYear = entity.OrgFiscalYear.FiscalYear.Name,
                    RowTimeStamp = entity.RowTimeStamp
                }
           );

            }

            return models;
        }

    }
}
