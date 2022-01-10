using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface ISubExpenseTypeBusiness : IBusiness<SubExpenseTypeDBEntity>
    {
        List<SubExpenseTypeDBEntity> GetSubExpenseTypesByExpenseId(int expenseTypeId, bool? active);
        List<SubExpenseTypeDBEntity> GetOrgId(int OrgId, bool? active);
    }
}
