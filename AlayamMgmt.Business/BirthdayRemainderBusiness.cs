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
    public class BirthdayRemainderBusiness : BaseBusiness<BirthdayRemainderDBEntity>, IBirthdayRemainderBusiness
    {
        public BirthdayRemainderBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<BirthdayRemainderDBEntity> GetTodayBirthdayFamilyMembers(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).ToList();
        }

        public int IsAnybodyBirthdayToday(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).Count();
        }
    }
}
