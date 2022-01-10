using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/TransactionReport")]
    public class TransactionReportController : ApiController
    {
        IOrgFiscalYearReportBusiness business = new OrgFiscalYearReportBusiness();

        [CustomAuthorizeAttribute]
        [Route("GetTransactionDetailReport")]
        public HttpResponseMessage GetTransactionDetailReport(int orgFiscalYearId)
        {
            HttpResponseMessage response;

            try
            {                
                List<TransactionDetailReportModel> models = getTransactionDetails(business.GetTransactionSummaryByDaily(orgFiscalYearId));
                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetTransactionDetailReportByDates")]
        [HttpPost]
        public HttpResponseMessage GetTransactionDetailReportByDates([FromBody]OrgDatesReportRequestModel model)
        {
            HttpResponseMessage response;

            try
            {
                List<TransactionDetailReportModel> models = getTransactionDetails(business.GetTransactionSummaryByDaily(model.OrganizationId, model.StartDate, model.EndDate));
                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [CustomAuthorizeAttribute]
        [Route("GetTransactionMonthlyReportByDates")]
        [HttpPost]
        public HttpResponseMessage GetTransactionMonthlyReportByDates([FromBody]OrgDatesReportRequestModel model)
        {
            HttpResponseMessage response;

            try
            {
                List<TransactionMonthlyReportModel> models = getTransactionsMonthly(business.GetTransactionSummaryByMonthly(model.OrganizationId, model.StartDate, model.EndDate));

                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionMonthlyReportByDates");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetTransactionMonthlyReport")]
       
        public HttpResponseMessage GetTransactionMonthlyReport(int orgFiscalYearId)
        {
            HttpResponseMessage response;

            try
            {                
                List<TransactionMonthlyReportModel> models = getTransactionsMonthly(business.GetTransactionSummaryByMonthly (orgFiscalYearId));


                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }



        [CustomAuthorizeAttribute]
        [Route("GetTransactionQuarterReport")]
        public HttpResponseMessage GetTransactionQuarterReport(int orgFiscalYearId)
        {
            HttpResponseMessage response;

            try
            {   
                List<TransactionQuarterReportModel> models = getTransactionsQuarter(business.GetTransactionSummaryByQuarter(orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [CustomAuthorizeAttribute]
        [Route("GetBalanceSheet")]
        public HttpResponseMessage GetBalanceSheet(int orgFiscalYearId)
        {
            HttpResponseMessage response;

            try
            {
                List<BalanceSheetModel> models = getBalanceSheetModels(business.GetBalanceSheet(orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetAccountBalanceSheet")]
        public HttpResponseMessage GetAccountBalanceSheet(int orgFiscalYearId)
        {
            HttpResponseMessage response;

            try
            {
                List<AccountBalanceSheetModel> models = getAccountBalanceSheetModels(business.GetAccountBalanceSheet(orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [CustomAuthorizeAttribute]
        [Route("GetQuarterBalanceSheet")]
        public HttpResponseMessage GetQuarterBalanceSheet(int orgFiscalYearId)
        {
            HttpResponseMessage response;

            try
            {
                List<QuarterBalanceSheetModel> models = getQuarterBalanceSheetModels(business.GetQuarterBalanceSheet(orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, models);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTransactionDetailReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        public List<TransactionDetailReportModel> getTransactionDetails(List<TransactionDetailReportDBEntity> entities)
        {
            List<TransactionDetailReportModel> models = new List<TransactionDetailReportModel>();
            entities.ForEach(x => models.Add(getTransactionDetail(x)));

            return models;
        }

        public TransactionDetailReportModel getTransactionDetail(TransactionDetailReportDBEntity entity)
        {
            return new TransactionDetailReportModel()
            {
                Account = entity.Account,
                Amount = entity.Amount,
                Date = entity.Date,
                Type = entity.Type
            };
        }

        public List<TransactionMonthlyReportModel> getTransactionsMonthly(List<TransactionMonthlyDBEntity> entities)
        {
            List<TransactionMonthlyReportModel> models = new List<TransactionMonthlyReportModel>();
            entities.ForEach(x => models.Add(getTransactionMonthly(x)));

            return models;
        }

        public TransactionMonthlyReportModel getTransactionMonthly(TransactionMonthlyDBEntity entity)
        {
            return new TransactionMonthlyReportModel()
            {
                Account = entity.Account,
                Amount = entity.Amount,
                Month = entity.Month,
                Type = entity.Type
            };
        }

        public List<TransactionQuarterReportModel> getTransactionsQuarter(List<TransactionQuarterDBEntity> entities)
        {
            List<TransactionQuarterReportModel> models = new List<TransactionQuarterReportModel>();
            entities.ForEach(x => models.Add(getTransactionQuarter(x)));

            return models;
        }

        public TransactionQuarterReportModel getTransactionQuarter(TransactionQuarterDBEntity entity)
        {
            return new TransactionQuarterReportModel()
            {
                Account = entity.Account,
                Amount = entity.Amount,
                Quarter = entity.Quarter,
                Type = entity.Type
            };
        }

        public List<BalanceSheetModel> getBalanceSheetModels(List<BalanceSheetDBEntity> entities)
        {
            List<BalanceSheetModel> models = new List<BalanceSheetModel>();
            entities.ForEach(x => models.Add(getBalanceSheetModel(x)));

            return models;
        }

        public BalanceSheetModel getBalanceSheetModel(BalanceSheetDBEntity entity)
        {
            return new BalanceSheetModel()
            {                
                Amount = entity.Amount,             
                Type = entity.Type,
                OrderId = entity.OrderId
            };
        }

        public List<AccountBalanceSheetModel> getAccountBalanceSheetModels(List<AccountBalanceSheetDBEntity> entities)
        {
            List<AccountBalanceSheetModel> models = new List<AccountBalanceSheetModel>();
            entities.ForEach(x => models.Add(getAccountBalanceSheetModel(x)));

            return models;
        }

        public AccountBalanceSheetModel getAccountBalanceSheetModel(AccountBalanceSheetDBEntity entity)
        {
            return new AccountBalanceSheetModel()
            {
                Amount = entity.Amount,
                Type = entity.Type,
                Account = entity.Account,
                OrderId = entity.OrderId
            };
        }


        public List<QuarterBalanceSheetModel> getQuarterBalanceSheetModels(List<QuarterBalanceSheetDBEntity> entities)
        {
            List<QuarterBalanceSheetModel> models = new List<QuarterBalanceSheetModel>();
            entities.ForEach(x => models.Add(getQuarterBalanceSheetModel(x)));

            return models;
        }

        public QuarterBalanceSheetModel getQuarterBalanceSheetModel(QuarterBalanceSheetDBEntity entity)
        {
            return new QuarterBalanceSheetModel()
            {
                Amount = entity.Amount,
                Type = entity.Type,
                Quarter = entity.Quarter,
                OrderId = entity.OrderId
            };
        }
    }
}
