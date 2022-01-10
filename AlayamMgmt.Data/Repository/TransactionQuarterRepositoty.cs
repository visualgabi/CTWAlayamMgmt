using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Repository
{
    public class TransactionQuarterRepositoty : IReportRepository<TransactionQuarterDBEntity> 
    {
         public DbContext context;

         public TransactionQuarterRepositoty()
        {
            this.context = new AlayamMgmtDBContext();            
        }

         public TransactionQuarterDBEntity Fetch(BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TransactionQuarterDBEntity> FetchAll(BaseCriteria criteria)
        {
            string sqlCommand = "EXEC uspTransactionSummaryByQuarter @OrgFiscalYearId";

            TransactionCriteria transactionCriteria =  (TransactionCriteria)criteria;

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", transactionCriteria.OrgFiscalYearId);

            List<TransactionQuarterDBEntity> entity = this.context.Database.SqlQuery<TransactionQuarterDBEntity>
                        (sqlCommand,                            
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<TransactionQuarterDBEntity>;
        }
    }
}
