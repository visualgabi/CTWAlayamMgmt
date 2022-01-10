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
    public class OrganizationBusiness : BaseBusiness<OrganizationDBEntity>, IOrganizationBusiness
    {
        public OrganizationBusiness(string aduitUser)
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

        //public byte[] Enable(int id, bool active)
        //{
        //    this._uow.GenericRepositoryObj.Enable(id, active);
        //    this._uow.Commit();

        //   OrganizationDBEntity entity = this._uow.GenericRepositoryObj.GetByID(id);
        //   return entity.RowTimeStamp;

        //}

        public override List<OrganizationDBEntity> Get(bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.ParentId == null && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.ParentId == null).ToList();
        }


        public List<OrganizationDBEntity> GetBranchesByOrgId(int orgId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.ParentId == orgId && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.ParentId == orgId).ToList();
        }
    }
}
