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
    public class OpeningBalanceBusiness : BaseBusiness<OpeningBalanceDBEntity>, IOpeningBalanceBusiness
    {
        public OpeningBalanceBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public List<OpeningBalanceDBEntity> GetByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId ).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId && x.Active == active).ToList();
        }
    }
}
