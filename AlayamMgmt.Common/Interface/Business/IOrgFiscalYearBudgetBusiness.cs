using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IOrgFiscalYearBudgetBusiness : IBusiness<OrgFiscalYearBudgetDBEntity>
    {
        List<OrgFiscalYearBudgetDBEntity> GetOrgFiscalYearBudgetsByOrgIdAndFiscalYearId(int orgId, int fiscalYearId, bool? active);
        List<OrgFiscalYearBudgetDBEntity> GetOrgFiscalYearBudgetsByOrgId(int orgId, bool? active);
        List<OrgFiscalYearBudgetDBEntity> GetCurrentYearBudgetsByOrgId(int orgId);
    }
}
