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
    public class TaxFilingBusiness : BaseBusiness<TaxFilingDBEntity>, ITaxFilingBusiness   
    {
        public TaxFilingBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<TaxFilingDBEntity> GetOrgId(int OrgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == OrgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == OrgId && x.Active == active.Value).ToList();
        }


        public DateTime GetLastTaxFiledFiscalYearEndDateByOrgId(int OrgId)
        {            
            TaxFilingDBEntity entity = this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == OrgId && x.Active == true).OrderByDescending( y => y.OrgFiscalYear.FiscalYearEnd).Take(1).FirstOrDefault();

            if (entity == null)
                return DateTime.Now.AddYears(-5);
            else
                return entity.OrgFiscalYear.FiscalYearEnd;
        }
    }
}
