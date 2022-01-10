using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IOfferingBusiness : IBusiness<OfferingDBEntity>   
    {
        List<OfferingDBEntity> GetByOrgId(int orgId, bool? active);

        List<OfferingDBEntity> GetByOrgIdWithPaging(int orgId, bool? active, int page, int pageSize);

        List<OfferingDBEntity> GetThisYearOfferingByOrgId(int orgId, bool? active);

        List<OfferingDBEntity> GetRecentOfferingsByOrgId(int orgId);

        List<OfferingDBEntity> GetByFamilyId(int familyId, DateTime OfferingStartDate, DateTime OfferingEndDate);
        List<OfferingDBEntity> GetByFamilyMemberId(int familyMemberId, DateTime OfferingStartDate, DateTime OfferingEndDate);
        List<OfferingDBEntity> GetBySponsorId(int sponsorId, DateTime OfferingStartDate, DateTime OfferingEndDate);

        List<OfferingDBEntity> GetFiscalOfferingByFamilyId(int familyId, int orgFiscalYearId);
        List<OfferingDBEntity> GetFiscalOfferingByFamilyMemberId(int familyMemberId, int orgFiscalYearId);
        List<OfferingDBEntity> GetFiscalOfferingBySponsorId(int sponsorId, int orgFiscalYearId);

        List<OfferingDBEntity> Report(int orgId, DateTime OfferingStartDate, DateTime OfferingEndDate, int OfferingSource, List<int> OfferingType, List<int> FundType, List<int> PaymentType);
        List<OfferingDBEntity> Report(int orgId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? orderById, int? sourceId, int? fundTypeId, int? offeringTypeId);
        
        List<OfferingDBEntity> FamilyOfferingReport(int familyId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? fundTypeId, int? offeringTypeId);
        List<OfferingDBEntity> FamilyMemberOfferingReport(int familyMemberId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? fundTypeId, int? offeringTypeId);
        List<OfferingDBEntity> SponsorMemberOfferingReport(int sponsorId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? fundTypeId, int? offeringTypeId);
    }
}
