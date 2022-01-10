using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlayamMgmt.BusinessLogic
{
    public class EmailContentBusiness : BaseBusiness<EmailContentDBEntity>, IEmailContentBusiness
    {        

         public EmailContentBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public EmailContentDBEntity GetEmailContentByEmailType(int emailTypeId, int? OrgId, bool? active)
        {
            EmailContentDBEntity entity = null;

            if( OrgId == null)
            {
                if (active != null)
                {
                    entity = this._uow.GenericRepositoryObj.Get(x => x.EamilTypeId == emailTypeId && x.Active == active.Value).FirstOrDefault();
                }
                else
                {
                    entity = this._uow.GenericRepositoryObj.Get(x => x.EamilTypeId == emailTypeId).FirstOrDefault();  
                }
            }                
            else
                if (active != null)
                {
                    entity = this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId.Value && x.EamilTypeId == emailTypeId && x.Active == active.Value).FirstOrDefault();
                }
            else
                {
                    entity = this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId.Value && x.EamilTypeId == emailTypeId).FirstOrDefault();
                }

            return entity;
        }       
    }
}
