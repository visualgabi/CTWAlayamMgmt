using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IFamilyMemberBusiness : IBusiness<FamilyMemberDBEntity>   
    {
        List<FamilyMemberDBEntity> GetFamilyMembers(int familyId, bool? active);
        
        List<FamilyMemberDBEntity> GetFamilyMembersIncludeGroup(int familyId, bool? active);

        List<FamilyMemberDBEntity> GetByGreaterThanAge(int age);
        List<FamilyMemberDBEntity> GetByFamilyIdAndGreaterThanAge(int familyId, int age);
        List<FamilyMemberDBEntity> GetTaxPayerByOrgId(int orgId, bool? active);
        List<FamilyMemberDBEntity> GetTaxPayerByFamilyId(int familyId, bool? active);
        void MoveFamilyMember(int familyMemberId, int familyId, int relationshipId);        

        List<FamilyMemberDBEntity> GetMensByOrgId(int orgId, bool? active);
        List<FamilyMemberDBEntity> GetWomensByOrgId(int orgId, bool? active);

        List<FamilyMemberDBEntity> GetMensByBranchId(int branchId, bool? active);
        List<FamilyMemberDBEntity> GetWomensByBranchId(int branchId, bool? active);

        List<FamilyMemberDBEntity> GetFamilyMembersByEmailIds(List<string> emailIds);

        List<FamilyMemberDBEntity> GetFamilyMembersByIds(List<int> ids);

        List<FamilyMemberDBEntity> GetGroupFamilyMembersByOrgId(int OrgId);

        List<FamilyMemberDBEntity> GetMenGroupFamilyMembersByOrgId(int orgId);

        List<FamilyMemberDBEntity> GetWomenGroupFamilyMembersByOrgId(int orgId);

        List<FamilyMemberDBEntity> GetGroupFamilyMembersByBranchId(int BranchId);

        List<FamilyMemberDBEntity> GetMenGroupFamilyMembersByBranchId(int BranchId);

        List<FamilyMemberDBEntity> GetWomenGroupFamilyMembersByBranchId(int BranchId);

    }
}
