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
    public class SponsorBusiness: BaseBusiness<SponsorDBEntity>, ISponsorBusiness
    {
        public SponsorBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public bool IsUnique(int id, string name)
        {
            //while adding 
            if(id == 0)
                return this._uow.GenericRepositoryObj.Get(x => x.Name.Trim().ToLower() == name.Trim().ToLower()).Count() == 0;
            else
                //while editing 
                return this._uow.GenericRepositoryObj.Get(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.Id != id).Count() == 0;
        }

        public List<SponsorDBEntity> GetByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.Active == active).ToList();
        }
    }
}
