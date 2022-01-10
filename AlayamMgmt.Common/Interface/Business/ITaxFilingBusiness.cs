using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface ITaxFilingBusiness : IBusiness<TaxFilingDBEntity>   
    {
        List<TaxFilingDBEntity> GetOrgId(int OrgId, bool? active);
        DateTime GetLastTaxFiledFiscalYearEndDateByOrgId(int OrgId);
    }
}
