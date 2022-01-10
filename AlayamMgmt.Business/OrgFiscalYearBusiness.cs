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
    public class OrgFiscalYearBusiness : BaseBusiness<OrgFiscalYearDBEntity>, IOrgFiscalYearBusiness
    {
        public OrgFiscalYearBusiness(string aduitUser)
            : base(aduitUser)
        { }
    
        public List<OrgFiscalYearDBEntity> GetOrgFiscalYear(int OrgId, bool? active)
        {
            if(active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId && x.Active == active).ToList();
        }
    }
}
