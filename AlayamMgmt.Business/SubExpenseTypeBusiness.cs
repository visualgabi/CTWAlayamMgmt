using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class SubExpenseTypeBusiness : BaseBusiness<SubExpenseTypeDBEntity>, ISubExpenseTypeBusiness
    {
        public SubExpenseTypeBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<SubExpenseTypeDBEntity> GetSubExpenseTypesByExpenseId(int expenseTypeId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.ExpenseTypeId == expenseTypeId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.ExpenseTypeId == expenseTypeId && x.Active == active.Value).ToList();
        }


        public List<SubExpenseTypeDBEntity> GetOrgId(int OrgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.ExpenseType.OrganizationId == OrgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.ExpenseType.OrganizationId == OrgId && x.Active == active.Value).ToList();
        }
    }
}
