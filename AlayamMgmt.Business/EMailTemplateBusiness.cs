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
    public class EMailTemplateBusiness : BaseBusiness<EMailTemplateDBEntity>, IEMailTemplateBusiness
    {
        public EMailTemplateBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<EMailTemplateDBEntity> GetOrgId(int orgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.Active == active.Value).ToList();
        }

    }
}
