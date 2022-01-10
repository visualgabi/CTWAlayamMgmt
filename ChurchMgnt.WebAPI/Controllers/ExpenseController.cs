using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Mapper;
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
    [RoutePrefix("api/Expense")]
    public class ExpenseController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IExpenseBusiness expenseBusiness = new ExpenseBusiness(claimModel.LoginUserEmail);
                List<ExpenseModel> orgProfileModels = DBEntityToModel.GetExpenseModels(expenseBusiness.GetByOrgId(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int Id)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IExpenseBusiness expenseBusiness = new ExpenseBusiness(claimModel.LoginUserEmail);
                ExpenseModel expenseModel = DBEntityToModel.GetExpenseModel(expenseBusiness.GetById(Id));

                response = Request.CreateResponse(HttpStatusCode.OK, expenseModel);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        //[CustomAuthorizeAttribute]
        [Route("Report")]        
        [HttpPost]
        public HttpResponseMessage Report(ExpenseReportRequestModel searchModel)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                IExpenseBusiness expenseBusiness = new ExpenseBusiness(Constant.DEFAULT_EMAIL);
                List<ExpenseModel> expenseModels = DBEntityToModel.GetExpenseModels(expenseBusiness.Report(searchModel.OrganizationId, searchModel.ExpenseStartDate, searchModel.ExpenseEndDate, searchModel.ExpenseTypeId, searchModel.AccountId, searchModel.OrderById));

                response = Request.CreateResponse(HttpStatusCode.OK, expenseModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Report");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetRecentExpensesByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetRecentExpensesByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IExpenseBusiness expenseBusiness = new ExpenseBusiness(Constant.DEFAULT_EMAIL);
                List<ExpenseModel> expenseModels = DBEntityToModel.GetExpenseModels(expenseBusiness.GetRecentExpensesByOrgId(orgId));

                response = Request.CreateResponse(HttpStatusCode.OK, expenseModels);
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
                ClaimModel claimModel = GetClaimModel();
                IExpenseBusiness expenseBusiness = new ExpenseBusiness(claimModel.LoginUserEmail);
                byte[] returnValue = expenseBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save([FromBody]ExpenseModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();
                IExpenseBusiness expenseBusiness = new ExpenseBusiness(claimModel.LoginUserEmail);
                expenseBusiness.Save(GetExpenseDBEntity(model));

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

       

        private ExpenseDBEntity GetExpenseDBEntity(ExpenseModel model)
        {
            return new ExpenseDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                OrganizationId = model.OrganizationId,
                BranchId = model.BranchId,
                RowTimeStamp = model.RowTimeStamp,
                Id = model.Id,
                Amount = model.Amount,
                Notes = model.Notes,
                PaymentTypeId = model.PaymentTypeId,
                TransactionNumber = model.TransactionNumber,
                ExpenseTypeId = model.ExpenseTypeId,
                SubExpenseTypeId = model.SubExpenseTypeId,
                ExpenseDate = model.ExpenseDate,
                AccountId = model.AccountId
            };
        }
    }
}
