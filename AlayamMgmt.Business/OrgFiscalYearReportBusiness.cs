using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class OrgFiscalYearReportBusiness : IOrgFiscalYearReportBusiness
    {
        IOrgFiscalYearReportRepository repository = new OrgFiscalYearReportRepository();

        public List<TransactionMonthlyDBEntity> GetTransactionSummaryByMonthly(int id)
        {
            return repository.GetTransactionSummaryByMonthly(id);
        }

        public List<TransactionDetailReportDBEntity> GetTransactionSummaryByDaily(int id)
        {            
            return repository.GetTransactionSummaryByDaily(id);
        }

        public List<TransactionQuarterDBEntity> GetTransactionSummaryByQuarter(int id)
        {
            return repository.GetTransactionSummaryByQuarter(id);
        }

        public List<BalanceSheetDBEntity> GetBalanceSheet(int id)
        {
            return repository.GetBalanceSheet(id);
        }

        public List<AccountBalanceSheetDBEntity> GetAccountBalanceSheet(int id)
        {
            return repository.GetAccountBalanceSheet(id);
        }

        public List<QuarterBalanceSheetDBEntity> GetQuarterBalanceSheet(int id)
        {
            return repository.GetQuarterBalanceSheet(id);
        }


        public List<TransactionDetailReportDBEntity> GetTransactionSummaryByDaily(int OrganizationId, DateTime startDate, DateTime endDate)
        {
            return repository.GetTransactionSummaryByDaily(OrganizationId, startDate, endDate);
        }

        public List<TransactionMonthlyDBEntity> GetTransactionSummaryByMonthly(int OrganizationId, DateTime startDate, DateTime endDate)
        {
            return repository.GetTransactionSummaryByMonthly(OrganizationId, startDate, endDate);
        }
    }
}
