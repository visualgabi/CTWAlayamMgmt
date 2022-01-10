using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("OrgOffering")]
    public class OfferingDBEntity : BaseDBEntity
    {
        public OfferingDBEntity()
        { }
   
        [ForeignKey("FamilyMember"), Column("FamilyMemberId")]
        public int? FamilyMemberId { get; set; }

        [ForeignKey("Sponsor"), Column("SponsorId")]
        public int? SponsorId { get; set; }
     
        [Required]
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        [Required]
        [ForeignKey("OfferingType"), Column("OfferingTypeId")]
        public int OfferingTypeId { get; set; }

        [Required]
        [ForeignKey("FundType"), Column("FundTypeId")]
        public int FundTypeId { get; set; }

        [Required]
        [ForeignKey("PaymentType"), Column("PaymentTypeId")]
        public int PaymentTypeId { get; set; }

        public decimal Amount { get; set; }
        public DateTime OfferingDate { get; set; }

        public string TransactionNumber { get; set; }

        public string Notes { get; set; }

        public virtual FundTypeDBEntity FundType { get; set; }
        public virtual OfferingTypeDBEntity OfferingType { get; set; }
        public virtual FamilyMemberDBEntity FamilyMember { get; set; }
        public virtual OrganizationDBEntity Organization { get; set; }
        public virtual PaymentTypeDBEntity PaymentType { get; set; }
        public virtual SponsorDBEntity Sponsor { get; set; }
    }
}
