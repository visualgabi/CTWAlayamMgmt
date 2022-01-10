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
    public class ReportedIssueBusiness : BaseBusiness<ReportedIssueDBEntity>, IReportedIssueBusiness
    {
        public ReportedIssueBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public List<SponsorDBEntity> GetByOrgId(int orgId, bool? active)
        {
            throw new NotImplementedException();
        }
    }
}
