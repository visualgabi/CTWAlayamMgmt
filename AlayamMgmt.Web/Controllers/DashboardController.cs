using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlayamMgmt.Web.Mapper;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Web.Providers;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : BaseApiController
    {
        //[CustomAuthorizeAttribute]
        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetByOrgId(int orgId, bool? active)
        {
            LogHelper.WriteToLog(Level.Information, "Home page got fired", this.GetType(), "GetByOrgId");

            HttpResponseMessage response;
            try
            {
                MemberVisitoLineChartModel memVisModel = getMemAndVisLineChartData(orgId);
                List<OrgFiscalYearBudgetModel> budgetModels = getFiscalYearBudget(orgId);

                ICurrentYearReportBusiness currentYearReportBusiness = new CurrentYearReportBusiness();

                List<int> members = currentYearReportBusiness.GetMembersByMonthly().Select(x => x.Count).ToList();
                List<int> visitors = currentYearReportBusiness.GetVisitorsByMonthly().Select(x => x.Count).ToList();

                DashboardModel model = new DashboardModel();
                model.MemberListCount = members;
                model.VisitorListCount = visitors;
               
                model.Budgets = budgetModels.OrderBy(x => x.FundTypeId).Select( x => x.Amount).ToList();
                model.FundTypes = budgetModels.OrderBy(x => x.FundTypeId).Select(x => x.FundType).ToList();

                getFiscalYearExpense(orgId, model);
                getFiscalYearOffering(orgId, model);

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

        private void getFiscalYearExpense(int orgId, DashboardModel model)
        {
            List<ExpenseModel> models = new List<ExpenseModel>();

            ClaimModel claimModel = GetClaimModel();
            IExpenseBusiness business = new ExpenseBusiness(claimModel.LoginUserEmail);
            List<ExpenseDBEntity> entities = business.GetThisYearExpenseByOrgId(orgId);        

            IBusiness<ExpenseTypeDBEntity> expenseTypeBusiness = new BaseBusiness<ExpenseTypeDBEntity>(claimModel.LoginUserEmail);
            List<LookupModel> expenseTypeModels = GetLookModel<ExpenseTypeDBEntity>(expenseTypeBusiness.Get(true));

            model.ExpenseTypes = expenseTypeModels.OrderBy(x => x.Name).Select(x => x.Name).ToList();

            models = DBEntityToModel.GetExpenseModels(entities);
            List<ExpenseDataModel> expenses = models.GroupBy(x => x.ExpenseType).Select(y => new ExpenseDataModel { ExpenseType = y.Key, Amount = y.Sum(z => z.Amount) }).ToList();

            model.Expenses = expenses.OrderBy(x => x.ExpenseType).Select(y => y.Amount).ToList();
            model.ExpenseTypes = expenses.OrderBy(x => x.ExpenseType).Select(y => y.ExpenseType).ToList();
            
        }



        private void getFiscalYearOffering(int orgId, DashboardModel model)
        {
            List<OrgOfferingModel> models = new List<OrgOfferingModel>();

            ClaimModel claimModel = GetClaimModel();
            IOfferingBusiness business = new OfferingBusiness(claimModel.LoginUserEmail);
            List<OfferingDBEntity> entities = business.GetThisYearOfferingByOrgId(orgId, true);

            models = DBEntityToModel.GetOrgOfferingModels(entities);
            List<OfferingDataModel> offerings = models.GroupBy(x => x.FundType).Select(y => new OfferingDataModel { FundType = y.Key, Amount = y.Sum(z => z.Amount) }).ToList();

            foreach (string fundType in model.FundTypes)
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

            model.Offerings = offerings.OrderBy(x => x.FundType).Select(y => y.Amount).ToList();
            //models.GroupBy()
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

