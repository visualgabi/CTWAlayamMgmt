using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IFamilyBusiness : IBusiness<FamilyDBEntity>   
    {
        List<FamilyDBEntity> GetRecentMembersByOrgId(int orgId);
        List<FamilyDBEntity> GetRecentVisitorsByOrgId(int orgId);

        List<FamilyDBEntity> GetOnThisYearVisitors(int orgId);
        List<FamilyDBEntity> GetOnThisYearMembers(int orgId);

        List<FamilyDBEntity> GetMembers(int orgId, DateTime? membershipBeginDate, DateTime? membershipEndDate);

        List<FamilyDBEntity> GetVisitors(int orgId, DateTime? visitorBeginDate, DateTime? visitorEndDate);
        List<FamilyDBEntity> GetFamiliesByOrgId(int orgId, bool? active);

        List<FamilyDBEntity> GetFamiliesByBranchId(int branchId, bool? active);

        //  List<FamilyDBEntity> GetFamiliesByOrgId(int orgId, bool? active, bool includeFamilyMember, bool includeMemberGroup);

        List<FamilyDBEntity> GetFamiliesIncludeMembersByOrgId(int orgId, bool? active);

        List<FamilyDBEntity> GetFamiliesIncludeMembersAndGroupsByOrgId(int orgId, bool? active);
    }
}
