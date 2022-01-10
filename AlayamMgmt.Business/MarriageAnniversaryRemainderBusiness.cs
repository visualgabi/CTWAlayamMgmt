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
    public class MarriageAnniversaryRemainderBusiness : BaseBusiness<MarriageAnniversaryRemainderDBEntity>, IMarriageAnniversaryRemainderBusiness
    {
        public MarriageAnniversaryRemainderBusiness(string aduitUser)
            : base(aduitUser)
        { }      

        public List<MarriageAnniversaryRemainderDBEntity> GetTodayMarriageAnniversaryFamilies(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).ToList();
        }
      

        public int IsAnybodyMarriageAnniversarydayToday(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).Count();
        }
    }
}
