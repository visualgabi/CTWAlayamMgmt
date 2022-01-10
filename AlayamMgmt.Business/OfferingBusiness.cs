using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class OfferingBusiness : BaseBusiness<OfferingDBEntity>, IOfferingBusiness
    {
        public OfferingBusiness(string aduitUser)
            : base(aduitUser)
        { }
    


        public List<OfferingDBEntity> GetByOrgId(int orgId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.Active == active).ToList();
        }

        public List<OfferingDBEntity> GetByOrgIdWithPaging(int orgId, bool? active, int page, int pageSize)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId, null, page, pageSize, new SortExpression<OfferingDBEntity>(x => x.OfferingDate, ListSortDirection.Descending)).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.Active == active,null, page, pageSize, new SortExpression<OfferingDBEntity>(x => x.OfferingDate, ListSortDirection.Descending)).ToList();
        }

        public List<OfferingDBEntity> GetRecentOfferingsByOrgId(int orgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.Active == true).OrderByDescending(y => y.Id).Take(5).ToList();
        }

        public List<OfferingDBEntity> GetThisYearOfferingByOrgId(int orgId, bool? active)
        {
            DateTime beginDate = DateTime.Parse("01/01/" + DateTime.Now.Year.ToString());
            string dateFormat = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;            
            DateTime endDate = DateTime.Parse("12/31/" + DateTime.Now.Year.ToString());

            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == active).ToList();
        }

        public List<OfferingDBEntity> Report(int orgId, DateTime OfferingStartDate, DateTime OfferingEndDate)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM"); 
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).ToList();
        }


        public List<OfferingDBEntity> GetFiscalOfferingByFamilyId(int familyId, int orgFiscalYearId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.FamilyMember.FamilyId == familyId
                && x.Organization.FiscalYears.Where(y => y.Id == orgFiscalYearId && y.Active == true).First().FiscalYearStart >= x.OfferingDate
                && x.Organization.FiscalYears.Where(y => y.Id == orgFiscalYearId && y.Active == true).First().FiscalYearEnd >= x.OfferingDate
                && x.Active == true).ToList();
            
        }
        
        public List<OfferingDBEntity> GetFiscalOfferingByFamilyMemberId(int familyMemberId, int orgFiscalYearId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.FamilyMemberId == familyMemberId
                && x.Organization.FiscalYears.Where(y => y.Id == orgFiscalYearId && y.Active == true).First().FiscalYearStart >= x.OfferingDate
                && x.Organization.FiscalYears.Where(y => y.Id == orgFiscalYearId && y.Active == true).First().FiscalYearEnd >= x.OfferingDate
                && x.Active == true).ToList();
        }
        
        public List<OfferingDBEntity> GetFiscalOfferingBySponsorId(int sponsorId, int orgFiscalYearId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.SponsorId == sponsorId
                && x.Organization.FiscalYears.Where(y => y.Id == orgFiscalYearId && y.Active == true).First().FiscalYearStart >= x.OfferingDate
                && x.Organization.FiscalYears.Where(y => y.Id == orgFiscalYearId && y.Active == true).First().FiscalYearEnd >= x.OfferingDate
                && x.Active == true).ToList();
        }

        public List<OfferingDBEntity> Report(int orgId, DateTime OfferingStartDate, DateTime OfferingEndDate, int OfferingSource, List<int> OfferingTypes, List<int> FundTypes, List<int> PaymentTypes)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM"); 
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true
                    && (OfferingSource == 0 || (OfferingSource == 1 && x.FamilyMemberId != null && x.SponsorId == null) || (OfferingSource == 2 && x.FamilyMemberId == null && x.SponsorId != null)) 
                    && ((OfferingTypes.Count > 0 && OfferingTypes.Contains(x.OfferingTypeId)) || OfferingTypes.Count == 0)
                    && ((FundTypes.Count > 0 && FundTypes.Contains(x.FundTypeId)) || FundTypes.Count == 0)
                    && ((PaymentTypes.Count > 0 && PaymentTypes.Contains(x.PaymentTypeId)) || PaymentTypes.Count == 0)
                ).ToList();   
        }        

        public List<OfferingDBEntity> Report(int orgId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? orderById, int? sourceId, int? fundTypeId, int? offeringTypeId)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            if (orderById == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                    && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null) )))
                    && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId) )
                    && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                    && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).ToList();
            else
            {
                switch (orderById)
                {
                    case 1:
                        return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                            && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null))))
                            && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                            && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                            && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.FamilyMember.FirstName).ToList();
                    case 2:
                        return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                            && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null))))
                            && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                            && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                            && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.OfferingType.Name).ToList();
                    case 3:
                        return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                            && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null))))
                            && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                            && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                            && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.FundType.Name).ToList();
                    case 4:
                        return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                            && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null))))
                            && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                            && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                            && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.OfferingDate).ToList();
                    case 5:
                        return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                            && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null))))
                            && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                            && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                            && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.Amount).ToList();
                    default:
                        return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId
                            && (sourceId == null || (sourceId != null && ((sourceId == 1 && x.FamilyMemberId != null) || (sourceId == 2 && x.FamilyMemberId == null))))
                            && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                            && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                            && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).ToList();
                }
            }
        }
        
        
        public List<OfferingDBEntity> GetByFamilyId(int familyId, DateTime OfferingStartDate, DateTime OfferingEndDate)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.FamilyMember.Family.Id == familyId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.Id).ToList();   
        }

        public List<OfferingDBEntity> GetByFamilyMemberId(int familyMemberId, DateTime OfferingStartDate, DateTime OfferingEndDate)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.FamilyMemberId == familyMemberId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.Id).ToList();   
        }

        public List<OfferingDBEntity> GetBySponsorId(int sponsorId, DateTime OfferingStartDate, DateTime OfferingEndDate)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.SponsorId == sponsorId && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(y => y.Id).ToList();  
        }


        public List<OfferingDBEntity> FamilyOfferingReport(int familyId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? fundTypeId, int? offeringTypeId)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.FamilyMember.FamilyId == familyId                
                && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(x => x.OfferingDate).ToList();    
        }

        public List<OfferingDBEntity> FamilyMemberOfferingReport(int familyMemberId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? fundTypeId, int? offeringTypeId)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.FamilyMemberId == familyMemberId
                && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(x => x.OfferingDate).ToList();    
        }

        public List<OfferingDBEntity> SponsorMemberOfferingReport(int sponsorId, DateTime OfferingStartDate, DateTime OfferingEndDate, int? fundTypeId, int? offeringTypeId)
        {
            DateTime beginDate = DateTime.Parse(OfferingStartDate.ToShortDateString() + " 00 AM");
            DateTime endDate = DateTime.Parse(OfferingEndDate.ToShortDateString() + " 12 PM");

            return this._uow.GenericRepositoryObj.Get(x => x.SponsorId == sponsorId
                && (fundTypeId == null || (fundTypeId != null && x.FundTypeId == fundTypeId))
                && (offeringTypeId == null || (offeringTypeId != null && x.OfferingTypeId == offeringTypeId))
                && x.OfferingDate >= beginDate && x.OfferingDate <= endDate && x.Active == true).OrderBy(x => x.OfferingDate).ToList();    
        }       
    }
}
