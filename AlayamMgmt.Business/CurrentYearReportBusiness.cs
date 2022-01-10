using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class CurrentYearReportBusiness : ICurrentYearReportBusiness
    {
        ICurrentYearReportRepository repository = new CurrentYearReportRepository();

        public List<Common.DBEntity.MemberOrVisitorDBEntity> GetMembersByMonthly()
        {
           return repository.GetMembersByMonthly();
        }

        public List<Common.DBEntity.MemberOrVisitorDBEntity> GetVisitorsByMonthly()
        {
            return repository.GetVisitorsByMonthly();
        }
    }
}
