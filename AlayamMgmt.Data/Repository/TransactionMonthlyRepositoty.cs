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
    public class TransactionMonthlyRepositoty : IReportRepository<TransactionMonthlyDBEntity> 
    {
         public DbContext context;

         public TransactionMonthlyRepositoty()
        {
            this.context = new AlayamMgmtDBContext();            
        }

         public TransactionMonthlyDBEntity Fetch(BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TransactionMonthlyDBEntity> FetchAll(BaseCriteria criteria)
        {
            string sqlCommand = "EXEC uspTransactionSummaryByMonthly @OrgFiscalYearId";

            TransactionCriteria transactionCriteria =  (TransactionCriteria)criteria;

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", transactionCriteria.OrgFiscalYearId);

            List<TransactionMonthlyDBEntity> entity = this.context.Database.SqlQuery<TransactionMonthlyDBEntity>
                        (sqlCommand,                            
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<TransactionMonthlyDBEntity>;
        }
    }
}
