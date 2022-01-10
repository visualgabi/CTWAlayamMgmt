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
using ChurchMgnt.WebAPI.Mapper;
using AlayamMgmt.BusinessLogic;
using ChurchMgnt.WebAPI.Providers;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetYearlyFinanceSummary")]
        [HttpGet]
        public HttpResponseMessage GetYearlyFinanceSummary(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                MemberVisitorGraphModel model = new MemberVisitorGraphModel();
                ICurrentYearReportBusiness currentYearReportBusiness = new CurrentYearReportBusiness();

                model.MemberListCount = currentYearReportBusiness.GetMembersByMonthly().Select(x => x.Count).ToList();
                model.VisitorListCount = currentYearReportBusiness.GetVisitorsByMonthly().Select(x => x.Count).ToList();

                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetMonthlyFinanceSummary")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyFinanceSummary(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                MemberVisitorGraphModel model = new MemberVisitorGraphModel();
                ICurrentYearReportBusiness currentYearReportBusiness = new CurrentYearReportBusiness();

                model.MemberListCount = currentYearReportBusiness.GetMembersByMonthly().Select(x => x.Count).ToList();
                model.VisitorListCount = currentYearReportBusiness.GetVisitorsByMonthly().Select(x => x.Count).ToList();

                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetLastWeekFinanceSummary")]
        [HttpGet]
        public HttpResponseMessage GetLastWeekFinanceSummary(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                MemberVisitorGraphModel model = new MemberVisitorGraphModel();
                ICurrentYearReportBusiness currentYearReportBusiness = new CurrentYearReportBusiness();

                model.MemberListCount = currentYearReportBusiness.GetMembersByMonthly().Select(x => x.Count).ToList();
                model.VisitorListCount = currentYearReportBusiness.GetVisitorsByMonthly().Select(x => x.Count).ToList();

                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetMemberVisitorCountGraph")]
        [HttpGet]
        public HttpResponseMessage GetMemberVisitorCountGraph(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                MemberVisitorGraphModel model = new MemberVisitorGraphModel();
                ICurrentYearReportBusiness currentYearReportBusiness = new CurrentYearReportBusiness();

                model.MemberListCount = currentYearReportBusiness.GetMembersByMonthly().Select(x => x.Count).ToList();
                model.VisitorListCount = currentYearReportBusiness.GetVisitorsByMonthly().Select(x => x.Count).ToList();                
              
                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        //[CustomAuthorizeAttribute]
        [CustomAuthorizeAttribute]
        [Route("GetBudgetGraph")]
        [HttpGet]
        public HttpResponseMessage GetBudgetGraph(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                BudgetGraphModel model = new BudgetGraphModel();

                List<OrgFiscalYearBudgetModel> budgetModels = getFiscalYearBudget(orgId);
                Random random = new Random();
                model.Budgets = budgetModels.OrderBy(x => x.FundTypeId).Select(x => new BudgetDataModel() { Value = x.Amount, Label = x.FundType, Color = String.Format("#{0:X6}", random.Next(0x1000000)), Highlight = String.Format("#{0:X6}", new Random().Next(0x1000000)) }).ToList();
                
                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        //[CustomAuthorizeAttribute]
        [CustomAuthorizeAttribute]
        [Route("GetExpenseGraph")]
        [HttpGet]
        public HttpResponseMessage GetExpenseGraph(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                ExpenseGraphModel model = new ExpenseGraphModel();

                model.Expenses = getFiscalYearExpense(orgId);
                
                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        //[CustomAuthorizeAttribute]
        [CustomAuthorizeAttribute]
        [Route("GetFundTypeOfferingGraph")]
        [HttpGet]
        public HttpResponseMessage GetFundTypeOfferingGraph(int orgId)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                FundTypeOfferingGraphModel model = new FundTypeOfferingGraphModel();
               
                List<OrgFiscalYearBudgetModel> budgetModels = getFiscalYearBudget(orgId);
                                
                model.Budgets = budgetModels.OrderBy(x => x.FundTypeId).Select(x => x.Amount).ToList();                
                model.FundTypes = budgetModels.OrderBy(x => x.FundTypeId).Select(x => x.FundType).ToList();

                model.Offerings = getFiscalYearOffering(orgId, model.FundTypes);               

                response = Request.CreateResponse(HttpStatusCode.OK, model);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentVisitors");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }
      

        private List<OrgFiscalYearBudgetModel> getFiscalYearBudget(int orgId)
        {
            List<OrgFiscalYearBudgetModel> models = new List<OrgFiscalYearBudgetModel>();

            ClaimModel claimModel = GetClaimModel();
            IOrgFiscalYearBudgetBusiness business = new OrgFiscalYearBudgetBusiness(claimModel.LoginUserEmail);
            List<OrgFiscalYearBudgetDBEntity> entities = business.GetCurrentYearBudgetsByOrgId(orgId);

            entities.ForEach(x => models.Add(ToOrgFiscalYearBudgetModel(x)));

            return models;
        }

        private List<ExpenseDataModel> getFiscalYearExpense(int orgId)
        {
            List<ExpenseModel> models = new List<ExpenseModel>();

            ClaimModel claimModel = GetClaimModel();
            IExpenseBusiness business = new ExpenseBusiness(claimModel.LoginUserEmail);
            List<ExpenseDBEntity> entities = business.GetThisYearExpenseByOrgId(orgId);      
            
            models = DBEntityToModel.GetExpenseModels(entities);
            Random random = new Random();
            return models.GroupBy(x => x.ExpenseType).Select(y => new ExpenseDataModel { Label = y.Key, Value = y.Sum(z => z.Amount), Color = String.Format("#{0:X6}", random.Next(0x1000000)) }).ToList();            
           
        }



        private List<decimal> getFiscalYearOffering(int orgId, List<string> fundTypes)
        {
            List<OrgOfferingModel> models = new List<OrgOfferingModel>();

            ClaimModel claimModel = GetClaimModel();
            IOfferingBusiness business = new OfferingBusiness(claimModel.LoginUserEmail);
            List<OfferingDBEntity> entities = business.GetThisYearOfferingByOrgId(orgId, true);

            models = DBEntityToModel.GetOrgOfferingModels(entities);
            List<OfferingDataModel> offerings = models.GroupBy(x => x.FundType).Select(y => new OfferingDataModel { FundType = y.Key, Amount = y.Sum(z => z.Amount) }).ToList();

            foreach (string fundType in fundTypes)
            {
                bool fundTypeAvailble = false;

                foreach (var offering in offerings.ToList())
                {
                    if (offering.FundType == fundType)
                    {
                        fundTypeAvailble = true;
                        break;
                    }
                }

                if (fundTypeAvailble == false)
                    offerings.Add(new OfferingDataModel() { FundType = fundType, Amount = 0 });
            }

            return offerings.OrderBy(x => x.FundType).Select(y => y.Amount).ToList();
            
        }


        private MemberVisitoLineChartModel getMemAndVisLineChartData(int orgId)
        {
            ClaimModel claimModel = GetClaimModel();
            IFamilyBusiness familyBusiness = new FamilyBusiness(claimModel.LoginUserEmail);
            List<FamilyDBEntity> visitors = familyBusiness.GetOnThisYearVisitors(orgId);
            List<FamilyDBEntity> members = familyBusiness.GetOnThisYearMembers(orgId);

            MemberVisitoLineChartModel model = GetMembersAndVisitorsLineChartModel(visitors, members);

            return model;
        }

        private OrgFiscalYearBudgetModel ToOrgFiscalYearBudgetModel(OrgFiscalYearBudgetDBEntity entity)
        {
            return new OrgFiscalYearBudgetModel()
            {
                Active = entity.Active,
                Amount = entity.Amount,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                FundType = entity.FundType.Name,
                FundTypeId = entity.FundTypeId,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser
            };
        }

        private MemberVisitoLineChartModel GetMembersAndVisitorsLineChartModel(List<FamilyDBEntity> visitorEntities, List<FamilyDBEntity> memberEntities)
        {
            MemberVisitoLineChartModel model = new MemberVisitoLineChartModel();

            for (int iMonth = 1; iMonth <= DateTime.Now.Month; iMonth++)
            {
                model.Labels.Add(DateTimeHelper.MonthName(iMonth));
                model.Visitors.Add(new LineChartModel() { Month = DateTimeHelper.MonthName(iMonth), Count = 0 });
                model.Members.Add(new LineChartModel() { Month = DateTimeHelper.MonthName(iMonth), Count = 0 });
            }

            model.Series.Add("Members");
            model.Series.Add("Visitors");

            List<LineChartModel> visitorsGroupByMonth = visitorEntities.GroupBy(x => x.FirstVisitedDate.Month)
                .Select(y => new LineChartModel { Month = DateTimeHelper.MonthName(y.Key), Count = y.Count() }).
                OrderBy(j => j.Month).ToList<LineChartModel>();

            List<LineChartModel> membersGroupByMonth = memberEntities.GroupBy(x => x.MembershipStartDate.Value.Month)
              .Select(y => new LineChartModel { Month = DateTimeHelper.MonthName(y.Key), Count = y.Count() }).
              OrderBy(j => j.Month).ToList<LineChartModel>();

            foreach (LineChartModel visitorGroupByMonth in visitorsGroupByMonth)
            {
                if (model.Visitors.Where(x => x.Month == visitorGroupByMonth.Month).Count() > 0)
                    model.Visitors.Where(x => x.Month == visitorGroupByMonth.Month).First().Count = visitorGroupByMonth.Count;
            }

            foreach (LineChartModel memberGroupByMonth in membersGroupByMonth)
            {
                if (model.Members.Where(x => x.Month == memberGroupByMonth.Month).Count() > 0)
                    model.Members.Where(x => x.Month == memberGroupByMonth.Month).First().Count = memberGroupByMonth.Count;
            }            

            for (int iMonth = 1; iMonth <= 12; iMonth++)
            {
                if (model.Labels.Where(x => x == DateTimeHelper.MonthName(iMonth)).Count() == 0)
                    model.Labels.Add(DateTimeHelper.MonthName(iMonth));
            }

            model.MemberCount = model.Members.Select(x => x.Count).ToList<int>();
            model.VisitorCount = model.Visitors.Select(x => x.Count).ToList<int>();            

            return model;
        }
    }
}

