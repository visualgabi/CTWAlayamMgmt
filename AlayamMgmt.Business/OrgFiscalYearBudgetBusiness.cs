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
    public class OrgFiscalYearBudgetBusiness : BaseBusiness<OrgFiscalYearBudgetDBEntity>, IOrgFiscalYearBudgetBusiness
    {
        public OrgFiscalYearBudgetBusiness(string aduitUser)
            : base(aduitUser)
        { }
    
        public List<OrgFiscalYearBudgetDBEntity> GetOrgFiscalYearBudgetsByOrgIdAndFiscalYearId(int orgId, int fiscalYearId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId && x.OrgFiscalYearId == fiscalYearId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId && x.OrgFiscalYearId == fiscalYearId && x.Active == true).ToList();
        }


        public List<OrgFiscalYearBudgetDBEntity> GetOrgFiscalYearBudgetsByOrgId(int orgId, bool? active)
        {
            if(active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId ).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId && x.Active == active).ToList();
        }


        public List<OrgFiscalYearBudgetDBEntity> GetCurrentYearBudgetsByOrgId(int orgId)
        {

            return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId && x.OrgFiscalYear.FiscalYearStart <= DateTime.Now && DateTime.Now <= x.OrgFiscalYear.FiscalYearEnd && x.Active == true).ToList();
        }
    }
}
