using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IDepositBusiness : IBusiness<DepositDBEntity>
    {
        List<DepositDBEntity> GetOrgId(int OrgId, bool? active);
        List<DepositDBEntity> GetRecentDepositsByOrgId(int OrgId);
        List<DepositDBEntity> Report(int orgId, DateTime depositStartDate, DateTime depositEndDate, int? accountId, int? userId, int? orderById);
    }
}
