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
    public class OrgFiscalYearReportRepository : IOrgFiscalYearReportRepository
    {
        public DbContext context;

        public OrgFiscalYearReportRepository()
        {
            this.context = new AlayamMgmtDBContext();            
        }

        public List<TransactionMonthlyDBEntity> GetTransactionSummaryByMonthly(int id)
        {
            string sqlCommand = "EXEC uspTransactionSummaryByMonthly @OrgFiscalYearId";

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", id);

            List<TransactionMonthlyDBEntity> entity = this.context.Database.SqlQuery<TransactionMonthlyDBEntity>
                        (sqlCommand,
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<TransactionMonthlyDBEntity>;
        }

        public List<TransactionDetailReportDBEntity> GetTransactionSummaryByDaily(int id)
        {
            string sqlCommand = "EXEC uspTransactionDetails @OrgFiscalYearId";

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", id);


            List<TransactionDetailReportDBEntity> entity = this.context.Database.SqlQuery<TransactionDetailReportDBEntity>
                        (sqlCommand,
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<TransactionDetailReportDBEntity>;
        }

        public List<TransactionQuarterDBEntity> GetTransactionSummaryByQuarter(int id)
        {
            string sqlCommand = "EXEC uspTransactionSummaryByQuarter @OrgFiscalYearId";

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", id);

            List<TransactionQuarterDBEntity> entity = this.context.Database.SqlQuery<TransactionQuarterDBEntity>
                        (sqlCommand,
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<TransactionQuarterDBEntity>;
        }

        public List<BalanceSheetDBEntity> GetBalanceSheet(int id)
        {
            string sqlCommand = "EXEC uspBalanceSheet @OrgFiscalYearId";

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", id);

            List<BalanceSheetDBEntity> entity = this.context.Database.SqlQuery<BalanceSheetDBEntity>
                        (sqlCommand,
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<BalanceSheetDBEntity>;
        }


        public List<AccountBalanceSheetDBEntity> GetAccountBalanceSheet(int id)
        {
            string sqlCommand = "EXEC uspBalanceSheetByAccount @OrgFiscalYearId";

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", id);

            List<AccountBalanceSheetDBEntity> entity = this.context.Database.SqlQuery<AccountBalanceSheetDBEntity>
                        (sqlCommand,
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<AccountBalanceSheetDBEntity>;
        }

        public List<QuarterBalanceSheetDBEntity> GetQuarterBalanceSheet(int id)
        {
            string sqlCommand = "EXEC uspBalanceSheetByQuarter @OrgFiscalYearId";

            var pOrgFiscalYearId = new SqlParameter("OrgFiscalYearId", id);

            List<QuarterBalanceSheetDBEntity> entity = this.context.Database.SqlQuery<QuarterBalanceSheetDBEntity>
                        (sqlCommand,
                            pOrgFiscalYearId
                          ).ToList();

            return entity as List<QuarterBalanceSheetDBEntity>;
        }


        public List<TransactionDetailReportDBEntity> GetTransactionSummaryByDaily(int organizationId, DateTime startDate, DateTime endDate)
        {
            string sqlCommand = "EXEC uspTransactionDetailsByDates @OrganizationId, @StartDate, @EndDate";

            var pOrganizationId = new SqlParameter("OrganizationId", organizationId);
            var pStartDate = new SqlParameter("StartDate", startDate);
            var pEndDate = new SqlParameter("EndDate", endDate);


            List<TransactionDetailReportDBEntity> entity = this.context.Database.SqlQuery<TransactionDetailReportDBEntity>
                        (sqlCommand,
                            pOrganizationId, pStartDate, pEndDate
                          ).ToList();

            return entity as List<TransactionDetailReportDBEntity>;
        }

        public List<TransactionMonthlyDBEntity> GetTransactionSummaryByMonthly(int organizationId, DateTime startDate, DateTime endDate)
        {
            string sqlCommand = "EXEC uspTransactionMonthlySummaryByDates @OrganizationId, @StartDate, @EndDate";

            var pOrganizationId = new SqlParameter("OrganizationId", organizationId);
            var pStartDate = new SqlParameter("StartDate", startDate);
            var pEndDate = new SqlParameter("EndDate", endDate);


            List<TransactionMonthlyDBEntity> entity = this.context.Database.SqlQuery<TransactionMonthlyDBEntity>
                        (sqlCommand,
                            pOrganizationId, pStartDate, pEndDate
                          ).ToList();

            return entity as List<TransactionMonthlyDBEntity>;
        }
    }
}
