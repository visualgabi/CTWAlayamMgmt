using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class FamilyMemberBusiness : BaseBusiness<FamilyMemberDBEntity>, IFamilyMemberBusiness
    {
        public FamilyMemberBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<FamilyMemberDBEntity> GetFamilyMembers(int familyId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId).ToList();
        }

        public List<FamilyMemberDBEntity> GetMensByOrgId(int orgId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.ParentId == orgId && x.Gender == "M" && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.ParentId == orgId && x.Gender == "M").ToList();
        }

        public List<FamilyMemberDBEntity> GetWomensByOrgId(int orgId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.ParentId == orgId && x.Gender == "F" && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.ParentId == orgId && x.Gender == "F").ToList();
        }

        public List<FamilyMemberDBEntity> GetMensByBranchId(int branchId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == branchId && x.Gender == "M" && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == branchId && x.Gender == "M").ToList();
        }

        public List<FamilyMemberDBEntity> GetWomensByBranchId(int branchId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == branchId && x.Gender == "F" && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == branchId && x.Gender == "F").ToList();
        }


        public List<FamilyMemberDBEntity> GetByGreaterThanAge(int age)
        {
            return _uow.GenericRepositoryObj.Get(x => x.Active == true && (DateTime.Now.Year - x.DOB.Year ) > age).ToList();
        }

        public List<FamilyMemberDBEntity> GetByFamilyIdAndGreaterThanAge(int familyId, int age)
        {
            return _uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.Active == true && (DateTime.Now.Year - x.DOB.Year) > age).ToList();
        }

        public List<FamilyMemberDBEntity> GetTaxPayerByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.ParentId == orgId && x.IsTaxPayer == true).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.ParentId == orgId && x.IsTaxPayer == true && x.Active == active).ToList();
        }

       

        public new void Save(FamilyMemberDBEntity entity)
        { 
            foreach (GroupMemberDBEntity groupMemberEntity in entity.GroupMembers)
            {
                _uow.GroupMemberRepositoryObj.Save(groupMemberEntity);
            }

            _uow.GenericRepositoryObj.Save(entity);

            _uow.Commit();
        }

        public void MoveFamilyMember(int familyMemberId, int familyId, int relationshipId)
        {
            FamilyMemberDBEntity entity = _uow.GenericRepositoryObj.Get(x => x.Id == familyMemberId).FirstOrDefault();
            entity.FamilyId = familyId;
            entity.RelationshipId = relationshipId;

            _uow.GenericRepositoryObj.Save(entity);

            _uow.Commit();
        }

        public List<FamilyMemberDBEntity> GetFamilyMembersIncludeGroup(int familyId, bool? active)
        {
			List<FamilyMemberDBEntity> familyMemberDBEntities = new List<FamilyMemberDBEntity>();

			if (active != null)
				familyMemberDBEntities = _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.FamilyId == familyId && x.Active == active.Value, null, y => y.GroupMembers).ToList();
            else
				familyMemberDBEntities = _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.FamilyId == familyId, null, y => y.GroupMembers).ToList();

			string str = JsonConvert.SerializeObject(familyMemberDBEntities);

			return familyMemberDBEntities;

		}

        public List<FamilyMemberDBEntity> GetFamilyMembersByEmailIds(List<string> emailIds)
        {
            return _uow.GenericRepositoryObj.Get(x => emailIds.Contains( x.EmailId1 )).ToList();
        }

        public List<FamilyMemberDBEntity> GetFamilyMembersByIds(List<int> ids)
        {
            return _uow.GenericRepositoryObj.Get(x => ids.Contains(x.Id)).ToList();
        }

        public List<FamilyMemberDBEntity> GetGroupFamilyMembersByOrgId(int orgId)
        {
            int ageLimit = int.Parse(ConfigurationManager.AppSettings["AgeLimitForGroupMember"].ToString());
            return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.Parent.Id == orgId && DbFunctions.DiffYears(x.DOB, DateTime.Today) > ageLimit && x.Active == true).ToList();
        }

        public List<FamilyMemberDBEntity> GetMenGroupFamilyMembersByOrgId(int orgId)
        {
            int ageLimit = int.Parse(ConfigurationManager.AppSettings["AgeLimitForGroupMember"].ToString());
            return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.Parent.Id == orgId && DbFunctions.DiffYears(x.DOB, DateTime.Today) > ageLimit && x.Gender == "M" && x.Active == true).ToList();
        }

        public List<FamilyMemberDBEntity> GetWomenGroupFamilyMembersByOrgId(int orgId)
        {
            int ageLimit = int.Parse(ConfigurationManager.AppSettings["AgeLimitForGroupMember"].ToString());
            return _uow.GenericRepositoryObj.Get(x => x.Family.Branch.Parent.Id == orgId && DbFunctions.DiffYears(x.DOB, DateTime.Today) > ageLimit && x.Gender == "F" && x.Active == true).ToList();
        }

        public List<FamilyMemberDBEntity> GetGroupFamilyMembersByBranchId(int BranchId)
        {
            int ageLimit = int.Parse(ConfigurationManager.AppSettings["AgeLimitForGroupMember"].ToString());
            return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == BranchId && DbFunctions.DiffYears(x.DOB, DateTime.Today) > ageLimit && x.Active == true).ToList();
        }

        public List<FamilyMemberDBEntity> GetMenGroupFamilyMembersByBranchId(int BranchId)
        {
            int ageLimit = int.Parse(ConfigurationManager.AppSettings["AgeLimitForGroupMember"].ToString());
            return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == BranchId && DbFunctions.DiffYears(x.DOB, DateTime.Today) > ageLimit && x.Gender == "M" && x.Active == true).ToList();
        }

        public List<FamilyMemberDBEntity> GetWomenGroupFamilyMembersByBranchId(int BranchId)
        {
            int ageLimit = int.Parse(ConfigurationManager.AppSettings["AgeLimitForGroupMember"].ToString());
            return _uow.GenericRepositoryObj.Get(x => x.Family.BranchId == BranchId && DbFunctions.DiffYears(x.DOB, DateTime.Today) > ageLimit && x.Gender == "F" && x.Active == true).ToList();
        }

        public List<FamilyMemberDBEntity> GetTaxPayerByFamilyId(int familyId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.IsTaxPayer == true).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.IsTaxPayer == true && x.Active == active).ToList();
        }
    }
}
