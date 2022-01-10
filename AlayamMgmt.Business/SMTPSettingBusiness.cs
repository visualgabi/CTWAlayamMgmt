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
    public class SMTPSettingBusiness : BaseBusiness<SMTPSettingDBEntity>, IOrgSMTPDetailsBusiness
    {
        public SMTPSettingBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public List<SMTPSettingDBEntity> GetOrgId(int OrgId, bool? active)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId && active == true).ToList();
        }
    }
}
