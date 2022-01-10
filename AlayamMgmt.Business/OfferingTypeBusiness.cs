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
    public class OfferingTypeBusiness : BaseBusiness<OfferingTypeDBEntity>, IOfferingTypeBusiness
    {
        public OfferingTypeBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public List<OfferingTypeDBEntity> GetOrgId(int OrgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == OrgId && x.Active == active.Value).ToList();
        }

        public bool IsUnique(int id, int orgId, string name)
        {
            if (id == 0)
                return this._uow.GenericRepositoryObj.Get(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.OrganizationId == orgId).Count() == 0;
            else
                //while editing 
                return this._uow.GenericRepositoryObj.Get(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.Id != id && x.OrganizationId == orgId).Count() == 0;
        }
    }
}
