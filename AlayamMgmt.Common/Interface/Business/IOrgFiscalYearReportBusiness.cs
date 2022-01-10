using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IOrgFiscalYearReportBusiness
    {
        List<TransactionDetailReportDBEntity> GetTransactionSummaryByDaily(int id);
        List<TransactionMonthlyDBEntity> GetTransactionSummaryByMonthly(int id);

        List<TransactionDetailReportDBEntity> GetTransactionSummaryByDaily(int OrganizationId, DateTime startDate, DateTime endDate);
        List<TransactionMonthlyDBEntity> GetTransactionSummaryByMonthly(int OrganizationId, DateTime startDate, DateTime endDate);

        List<TransactionQuarterDBEntity> GetTransactionSummaryByQuarter(int id);
        
        
        List<BalanceSheetDBEntity> GetBalanceSheet(int id);
        List<AccountBalanceSheetDBEntity> GetAccountBalanceSheet(int id);
        List<QuarterBalanceSheetDBEntity> GetQuarterBalanceSheet(int id);
    }
}
