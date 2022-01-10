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
    public class TransactionDetailRepositoty : IReportRepository<TransactionDetailReportDBEntity> 
    {
        public DbContext context;        

        public TransactionDetailRepositoty()
        {
            this.context = new AlayamMgmtDBContext();            
        }

        public TransactionDetailReportDBEntity Fetch(BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TransactionDetailReportDBEntity> FetchAll(BaseCriteria criteria)
        {
            string sqlCommand = "EXEC uspTransactionDetails @OrgFiscalYearId";

            TransactionCriteria transactionCriteria =  (TransactionCriteria)criteria;
                        
            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", transactionCriteria.OrgFiscalYearId);


            List<TransactionDetailReportDBEntity> entity = this.context.Database.SqlQuery<TransactionDetailReportDBEntity>
                        (sqlCommand,                            
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<TransactionDetailReportDBEntity>;
        }
    }
}
