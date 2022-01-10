using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IExpenseBusiness : IBusiness<ExpenseDBEntity>   
    {
        List<ExpenseDBEntity> GetByOrgId(int orgId, bool? active);
        List<ExpenseDBEntity> GetThisYearExpenseByOrgId(int orgId);

        List<ExpenseDBEntity> GetRecentExpensesByOrgId(int orgId);

        List<ExpenseDBEntity> Report(int orgId, DateTime ExpenseStartDate, DateTime ExpenseEndDate, List<int> ExpenseTypes, List<int> SubExpenseTypes, List<int> PaymentTypes);
        List<ExpenseDBEntity> Report(int orgId, DateTime ExpenseStartDate, DateTime ExpenseEndDate, int? ExpenseType, int? accountId, int? orderById);
    }
}
