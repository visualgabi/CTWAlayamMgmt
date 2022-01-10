using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IFundTypeBusiness : IBusiness<FundTypeDBEntity>   
    {
        List<FundTypeDBEntity> GetOrgId(int OrgId, bool? active);
        //bool IsUnique(int id, int orgId, string name);
    }
}
