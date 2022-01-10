using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class ExpenseBusiness : BaseBusiness<ExpenseDBEntity>, IExpenseBusiness
    {
        public ExpenseBusiness(string aduitUser)
            : base(aduitUser)
        { }



        public List<ExpenseDBEntity> GetByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId || x.Branch.ParentId == orgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => ( x.OrganizationId == orgId || x.Branch.ParentId == orgId ) && x.Active == active).ToList();
        }


        public List<ExpenseDBEntity> GetRecentExpensesByOrgId(int orgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || x.Branch.ParentId == orgId) && x.Active == true).OrderByDescending(y => y.Id).Take(5).ToList();
        }

        public List<ExpenseDBEntity> GetThisYearExpenseByOrgId(int orgId)
        {
            DateTime beginDate = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime endDate = DateTime.Parse("12/31/" + DateTime.Now.Year.ToString());

           // return this._uow.GenericRepositoryObj.Get(x => x.FiscalYear.OrganizationId == orgId && x.OrgFiscalYear.FiscalYearStart >= DateTime.Now && x.OrgFiscalYear.FiscalYearEnd <= DateTime.Now && x.Active == true).ToList();

            //return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId &&
            //    x.Organization.FiscalYears.Where(y => y.FiscalYearStart >= DateTime.Now && y.FiscalYearEnd <= DateTime.Now).First().FiscalYearStart >= x.ExpenseDate &&
            //    x.Organization.FiscalYears.Where(y => y.FiscalYearStart >= DateTime.Now && y.FiscalYearEnd <= DateTime.Now).First().FiscalYearEnd <= x.ExpenseDate
            //    && x.Active == true).ToList();

            return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true).ToList();
        }


        public List<ExpenseDBEntity> Report(int orgId, DateTime ExpenseStartDate, DateTime ExpenseEndDate, List<int> ExpenseTypes, List<int> SubExpenseTypes, List<int> PaymentTypes)
        {
            DateTime beginDate = DateTime.Parse(ExpenseStartDate.ToShortDateString() + " 00 AM"); 
            DateTime endDate = DateTime.Parse(ExpenseEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                    && ( ( ExpenseTypes.Count > 0 && ExpenseTypes.Contains(x.ExpenseTypeId) ) || ExpenseTypes.Count == 0)
                    && (( SubExpenseTypes.Count > 0 && SubExpenseTypes.Contains(x.SubExpenseTypeId)) || SubExpenseTypes.Count == 0)
                    && (( PaymentTypes.Count > 0 && PaymentTypes.Contains(x.PaymentTypeId)) || PaymentTypes.Count == 0)                                     
                ).ToList();   
        }

        public List<ExpenseDBEntity> Report(int orgId, DateTime ExpenseStartDate, DateTime ExpenseEndDate, int? ExpenseType, int? accountId, int? orderById)
        {
            DateTime beginDate = ExpenseStartDate;
            DateTime endDate = DateTime.Parse(ExpenseEndDate.ToShortDateString() + " 12 PM");

            if (orderById == null)
            {
                return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || ( x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                        && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                        && ((accountId != null && x.AccountId == accountId || accountId == null))
                    ).ToList();
            }
            else
            {  
                switch(orderById)
                {
                    case 1:
                        return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                            && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                            && ((accountId != null && x.AccountId == accountId || accountId == null))
                        ).OrderBy(y => y.ExpenseType.Name).ToList();
                    case 2:
                        return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                            && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                            && ((accountId != null && x.AccountId == accountId || accountId == null))
                        ).OrderBy(y => y.SubExpenseType.Name).ToList();
                    case 3:
                        return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                            && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                            && ((accountId != null && x.AccountId == accountId || accountId == null))
                        ).OrderBy(y => y.PaymentType.Name).ToList();
                    case 4:
                        return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                            && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                            && ((accountId != null && x.AccountId == accountId || accountId == null))
                        ).OrderBy(y => y.ExpenseDate).ToList();
                    case 5:
                        return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                            && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                            && ((accountId != null && x.AccountId == accountId || accountId == null))
                        ).OrderBy(y => y.Amount).ToList();
                    default:
                        return this._uow.GenericRepositoryObj.Get(x => (x.OrganizationId == orgId || (x.Branch != null && x.Branch.ParentId == orgId)) && x.ExpenseDate >= beginDate && x.ExpenseDate <= endDate && x.Active == true
                        && ((ExpenseType != null && x.ExpenseTypeId == ExpenseType || ExpenseType == null))
                        && ((accountId != null && x.AccountId == accountId || accountId == null))
                    ).ToList();

                }               
                 
            }
        }


        
    }
}
