using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.BusinessLogic;

namespace AlayamMgmt.Business
{
    public class GroupBusiness : BaseBusiness<GroupDBEntity>, IGroupBusiness
    {
        public GroupBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<GroupDBEntity> GetByOrgId(int orgId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId).ToList();
        }
    }
}
