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
    public class FamilyBusiness : BaseBusiness<FamilyDBEntity>, IFamilyBusiness
    {
        public FamilyBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<FamilyDBEntity> GetRecentMembersByOrgId(int orgId)
        {          
                return _uow.GenericRepositoryObj.Get(x => x.Branch.ParentId.Value == orgId && x.MembershipStartDate != null && x.Active == true).OrderByDescending(y => y.Id).Take(5).ToList();
        }

        public List<FamilyDBEntity> GetFamiliesByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId, null, x =>  x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).ToList();
            else
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId && x.Active == active, null, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).ToList();

            /*
            if (active == null)
                return _uow.GenericRepositoryObj.Get(x => x.Branch.ParentId.Value == orgId).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.Branch.ParentId.Value == orgId && x.Active == active).ToList();  
                */
        }


        public List<FamilyDBEntity> GetFamiliesIncludeMembersByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId, null, y => y.FamilyMembers, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).ToList();
            else
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId && x.Active == active, null, y => y.FamilyMembers, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).ToList();

            /*
            if (active == null)
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId, null, y => y.FamilyMembers).ToList();
            else
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId && x.Active == active, null, y => y.FamilyMembers).ToList();
                */
        }

        public List<FamilyDBEntity> GetFamiliesIncludeMembersAndGroupsByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId,null, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State, y => y.FamilyMembers.Select(x => x.Relationship), y => y.FamilyMembers.Select(z => z.GroupMembers.Select(x=> x.Group))).ToList();
            else
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId && x.Active == active, null, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State, y => y.FamilyMembers.Select(x => x.Relationship), y => y.FamilyMembers.Select(z => z.GroupMembers.Select(x => x.Group))).ToList();
        }



        public List<FamilyDBEntity> GetRecentVisitorsByOrgId(int orgId)
        {           
            return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.Branch.ParentId.Value == orgId && x.MembershipStartDate == null && x.Active == true, null, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).OrderByDescending(y => y.Id).Take(5).ToList();            
        }

        public List<FamilyDBEntity> GetMembers(int orgId, DateTime? membershipBeginDate, DateTime? membershipEndDate)
        {
            DateTime? beginDate = membershipBeginDate != null ? membershipBeginDate.Value : (DateTime?)null;
            DateTime? endDate = membershipEndDate != null ? DateTime.Parse(membershipEndDate.Value.ToShortDateString() + " 12 PM") : (DateTime?)null;

            List<FamilyDBEntity> entities = _uow.GenericRepositoryObj.Get(
                    x => x.Branch.ParentId.Value == orgId 
                    && x.MembershipStartDate != null 
                    && x.Active == true
                    && ( beginDate == null || (beginDate != null && x.MembershipStartDate >= beginDate ) )
                    && ( endDate == null   || (endDate != null && x.MembershipStartDate >= beginDate))
               ).OrderBy(x => x.CreateDateTime).ToList();
            return entities;

        }

        public List<FamilyDBEntity> GetVisitors(int orgId, DateTime? visitorBeginDate, DateTime? visitorEndDate)
        {
            DateTime? beginDate = visitorBeginDate != null ? visitorBeginDate.Value : (DateTime?)null;
            DateTime? endDate = visitorEndDate != null ? DateTime.Parse(visitorEndDate.Value.ToShortDateString() + " 12 PM") : (DateTime?)null;

            List<FamilyDBEntity> entities = _uow.GenericRepositoryObj.Get(x => x.Branch.ParentId.Value == orgId && x.MembershipStartDate == null && x.Active == true
                  && (beginDate == null || (beginDate != null && x.FirstVisitedDate >= beginDate))
                    && (endDate == null || (endDate != null && x.FirstVisitedDate >= beginDate))
                ).OrderBy(x => x.CreateDateTime).ToList();
            return entities;
        }



        public List<FamilyDBEntity> GetOnThisYearVisitors(int orgId)
        {
            DateTime beginDate = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime endDate = DateTime.Parse("12/31/" + DateTime.Now.Year.ToString());

            List<FamilyDBEntity> entities = _uow.GenericRepositoryObj.Get(x => x.Branch.ParentId.Value == orgId && x.MembershipStartDate == null && x.FirstVisitedDate >= beginDate
                && x.FirstVisitedDate <= endDate
                && x.Active == true).OrderBy(x => x.FirstVisitedDate).Take(5).ToList();

            return entities;
        }

        public List<FamilyDBEntity> GetOnThisYearMembers(int orgId)
        {
            DateTime beginDate = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            DateTime endDate = DateTime.Parse("12/31/" + DateTime.Now.Year.ToString());

            List<FamilyDBEntity> entities = _uow.GenericRepositoryObj.Get(x => x.Branch.ParentId.Value == orgId && x.MembershipStartDate != null && x.MembershipStartDate >= beginDate
               && x.MembershipStartDate <= endDate
               && x.Active == true).OrderBy(x => x.MembershipStartDate).Take(5).ToList();

            return entities;
        }

        public List<FamilyDBEntity> GetFamiliesByBranchId(int branchId, bool? active)
        {
            if (active == null)
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.BranchId == branchId, null, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).ToList();
            else
                return _uow.GenericRepositoryObj.GetWithIncludeAndNoTracking(x => x.BranchId == branchId && x.Active == active, null, x => x.Branch, x => x.Country, x => x.EthnicOrigin, x => x.MembershipStatus, x => x.PrimaryLanguage, x => x.SecondaryLanguage, x => x.State).ToList();
        }
    }
}

