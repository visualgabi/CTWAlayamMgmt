using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class OfferingReportRequestModel
    {
        public OfferingReportRequestModel()
        { }

        public int OrganizationId { get; set; }
        public DateTime StartDate {get; set;}
        public DateTime EndDate { get; set; }
        public int? OrderById { get; set; }
        public int? SourceId { get; set; }
        public int? FundTypeId { get; set; }
        public int? OfferingTypeId { get; set; }
    }

    public class FamilyOfferingReportRequestModel
    {
        public FamilyOfferingReportRequestModel()
        { }

        public int FamilyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
        public int? FundTypeId { get; set; }
        public int? OfferingTypeId { get; set; }
    }

    public class FamilyMemberOfferingReportRequestModel
    {
        public FamilyMemberOfferingReportRequestModel()
        { }

        public int FamilyMemberId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? FundTypeId { get; set; }
        public int? OfferingTypeId { get; set; }
    }

    public class SponsorOfferingReportRequestModel
    {
        public SponsorOfferingReportRequestModel()
        { }

        public int SponsorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? FundTypeId { get; set; }
        public int? OfferingTypeId { get; set; }
    }
}