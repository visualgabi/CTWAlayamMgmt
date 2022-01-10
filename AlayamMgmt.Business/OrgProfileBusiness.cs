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
    public class OrgProfileBusiness : BaseBusiness<OrgProfileDBEntity>, IOrgProfileBusiness
    {
        public OrgProfileBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public OrgProfileDBEntity GetProfile(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).FirstOrDefault();
        }
    }
}
