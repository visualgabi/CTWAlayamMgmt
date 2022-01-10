using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Repository
{
    public interface ICurrentYearReportRepository
    {
        List<MemberOrVisitorDBEntity> GetMembersByMonthly();
        List<MemberOrVisitorDBEntity> GetVisitorsByMonthly();
    }
}
