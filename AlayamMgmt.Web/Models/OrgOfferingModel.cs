using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class OrgOfferingModel : BaseModel
    {
        public OrgOfferingModel() { }
                
        public int? FamilyMemberId { get; set; }
        public int? SponsorId { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        [Required]
        public int OfferingTypeId { get; set; }
        [Required]
        public int FundTypeId { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OfferingDate { get; set; }
        public string TransactionNumber { get; set; }
        public string Notes { get; set; }

        public string FundType { get; set; }
        public string OfferingType { get; set; }
        public string FamilyMember { get; set; }
        public string Organization { get; set; }
        public string PaymentType { get; set; }
        public string Sponsor { get; set; }      

    }
}
