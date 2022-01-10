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
    public class DepositBusiness : BaseBusiness<DepositDBEntity>, IDepositBusiness
    {
        public DepositBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<DepositDBEntity> GetOrgId(int OrgId, bool? active)
        {
            if(active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == OrgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == OrgId && x.Active == active.Value).ToList();
        }

        public List<DepositDBEntity> GetRecentDepositsByOrgId(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == OrgId && x.Active == true).OrderByDescending(y => y.Id).Take(5).ToList();
        }


        public List<DepositDBEntity> Report(int orgId, DateTime depositStartDate, DateTime depositEndDate, int? accountId, int? userId, int? orderById)
        {
            DateTime beginDate = DateTime.Parse(depositStartDate.ToShortDateString() + " 00 AM"); 
            DateTime endDate = DateTime.Parse(depositEndDate.ToShortDateString() + " 12 PM");

            if (orderById == null)
            {
                return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == orgId && x.DepositDate >= beginDate && x.DepositDate <= endDate && x.Active == true
                        && ((accountId != null && x.AccountId == accountId.Value) || (accountId == null))
                        && ((userId != null && x.UserId == userId.Value) || (userId == null))
                    ).ToList();
            }
            else
            {
                switch (orderById)
                {
                    case 1:
                        return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == orgId && x.DepositDate >= beginDate && x.DepositDate <= endDate && x.Active == true
                       && ((accountId != null && x.AccountId == accountId.Value) || (accountId == null))
                       && ((userId != null && x.UserId == userId.Value) || (userId == null))
                   ).OrderBy(y => y.Account.Name).ToList();
                    case 2:
                        return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == orgId && x.DepositDate >= beginDate && x.DepositDate <= endDate && x.Active == true
                       && ((accountId != null && x.AccountId == accountId.Value) || (accountId == null))
                       && ((userId != null && x.UserId == userId.Value) || (userId == null))
                   ).OrderBy(y => y.User.FirstName).ToList();
                    case 3:
                        return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == orgId && x.DepositDate >= beginDate && x.DepositDate <= endDate && x.Active == true
                       && ((accountId != null && x.AccountId == accountId.Value) || (accountId == null))
                       && ((userId != null && x.UserId == userId.Value) || (userId == null))
                   ).OrderBy(y => y.Amount).ToList();
                    default:
                        return this._uow.GenericRepositoryObj.Get(x => x.Account.Bank.OrganizationId == orgId && x.DepositDate >= beginDate && x.DepositDate <= endDate && x.Active == true
                       && ((accountId != null && x.AccountId == accountId.Value) || (accountId == null))
                       && ((userId != null && x.UserId == userId.Value) || (userId == null))
                   ).ToList();
                }
            }
        }
        
    }
}
