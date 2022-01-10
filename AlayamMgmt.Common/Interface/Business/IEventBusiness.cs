using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IEventBusiness : IBusiness<EventDBEntity>   
    {
        List<EventDBEntity> GetBranchId(int branchId, bool? active);
        List<EventDBEntity> GetOrgId(int orgId, bool? active);
        List<EventDBEntity> Report(int orgId, DateTime eventStartDate, DateTime eventEndDate, int? eventType, bool? onlySplEvent, int? orderById);
        List<EventDBEntity> GetRecentEventsByOrgId(int orgId);
    }
}
