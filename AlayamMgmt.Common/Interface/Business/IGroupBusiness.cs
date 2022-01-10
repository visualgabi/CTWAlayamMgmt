using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IGroupBusiness : IBusiness<GroupDBEntity>
    {
        List<GroupDBEntity> GetByOrgId(int orgId, bool? active);

       // List<GroupDBEntity> GetCustomAndBuildGroupsByOrgId(int orgId, bool? active);
    }
}
