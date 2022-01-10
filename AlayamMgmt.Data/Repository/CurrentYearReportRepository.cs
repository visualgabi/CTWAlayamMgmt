using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Repository
{
    public class CurrentYearReportRepository : ICurrentYearReportRepository
    {
        public DbContext context;

        public CurrentYearReportRepository()
        {
            this.context = new AlayamMgmtDBContext();            
        }

        public List<MemberOrVisitorDBEntity> GetMembersByMonthly()
        {
            string sqlCommand = "EXEC uspGetCurrentYesarMonthlyMemberCount";

            List<MemberOrVisitorDBEntity> entity = this.context.Database.SqlQuery<MemberOrVisitorDBEntity>
                        (sqlCommand
                          ).ToList();

            return entity as List<MemberOrVisitorDBEntity>;
        }

        public List<MemberOrVisitorDBEntity> GetVisitorsByMonthly()
        {
            string sqlCommand = "EXEC uspGetCurrentYesarMonthlyVisitorCount";

            List<MemberOrVisitorDBEntity> entity = this.context.Database.SqlQuery<MemberOrVisitorDBEntity>
                        (sqlCommand
                          ).ToList();

            return entity as List<MemberOrVisitorDBEntity>;
        }
    }
}
