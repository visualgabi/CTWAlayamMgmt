using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlayamMgmt.Common.DBEntity
{
    [Table("Family")]
    public class FamilyDBEntity : BaseDBEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
               

        [Required]
        public DateTime FirstVisitedDate { get; set; }
                
        public DateTime? MembershipStartDate { get; set; }

        [Required]
        public int EthnicOriginId { get; set; }

        [Required]
        [ForeignKey("PrimaryLanguage"), Column("PrimaryLanguageId")]
        public int PrimaryLanguageId { get; set; }

        [ForeignKey("SecondaryLanguage"), Column("SecondaryLanguageId")]
        [Required]
        public int SecondaryLanguageId { get; set; }

        [Required]
        public int MembershipStatusId { get; set; }
                
        public int? BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        
        [StringLength(15)]
        public string Phone2 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }
                
        [StringLength(100)]
        public string EmailId2 { get; set; }

        //[ForeignKey("SecondaryLanguage"), Column("SecondaryLanguageId")]
        //[Required]
        //public List<int> SecondaryLanguageId { get; set; }

        public virtual List<FamilyMemberDBEntity> FamilyMembers { get; set; }

        public virtual EthnicOriginDBEntity EthnicOrigin { get; set; }

        public virtual LanguageDBEntity PrimaryLanguage { get; set; }

        public virtual LanguageDBEntity SecondaryLanguage { get; set; }

        public virtual CountryDBEntity Country { get; set; }

        public virtual StateDBEntity State { get; set; }

        public virtual MembershipStatusDBEntity MembershipStatus { get; set; }

        public virtual OrganizationDBEntity Branch { get; set; }

        public DateTime? MariageDate { get; set;}
        
    }
}
