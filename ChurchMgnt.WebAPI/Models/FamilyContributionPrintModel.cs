using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class ContributionPrintModel : PrintBaseModel
    {   
        public string OrgAddress1 { get; set; }

        public string OrgAddress2 { get; set; }

        public string OrgCity { get; set; }

        public string OrgState { get; set; }

        public string OrgCountry { get; set; }

        public string OrgZipCode { get; set; }

        public string OrgTaxId { get; set; }

        public string DonorName { get; set; }

        public string DonorAddress1 { get; set; }
        public string DonorAddress2 { get; set; }

        public string DonorCity { get; set; }

        public string DonorState { get; set; }

        public string DonorCountry { get; set; }

        public string DonorZipCode { get; set; }

        public string DonorType { get; set; }

        public string DonorEmailId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TotalContribution { get; set; }

        public string OrgContributionMsg { get; set; }

        public string OrgPastorName { get; set; }

        public string OrgSignatureLink { get; set; }

        public bool OrgIncludeSignature { get; set; }

        public string OrgLogoLink { get; set; }

        public string OrgWebsite { get; set; }

        public string OrgPhone { get; set; }

        public string OrgEmail { get; set; }        

        public List<ContributionOfferingModel> Offerings { get; set; }

    }    

    public class ContributionOfferingModel
    {
        public string PaymentType { get; set; }

        public decimal Amount { get; set; }

        public DateTime OfferingDate { get; set; }

        public string RefNo { get; set; }

    }
}