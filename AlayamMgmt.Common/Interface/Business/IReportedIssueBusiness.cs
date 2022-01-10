using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IReportedIssueBusiness : IBusiness<ReportedIssueDBEntity>
    {
         List<SponsorDBEntity> GetByOrgId(int orgId, bool? active);            
    }
}

