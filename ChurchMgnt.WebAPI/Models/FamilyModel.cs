using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class FamilyModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        public int EthnicOriginId { get; set; }

        [Required]
        public DateTime FirstVisitedDate {get; set;}

        [Required]
        public DateTime? MembershipStartDate { get; set; }

        [Required]
        public int PrimaryLanguageId { get; set; }

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

        [Required]
        public int StateId { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone2 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId2 { get; set; }

        public string EthnicOrigin { get; set; }

        public string PrimaryLanguage { get; set; }

        public string SecondaryLanguage { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string MembershipStatus { get; set; }

        public string Branch { get; set; }        

        public List<FamilyMemberModel> FamilyMembers { get; set; }

        public DateTime? MariageDate { get; set; }

        public string NameWithEmail1 { get; set; }
    }


    public class FamilyBasicModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
                
        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }
                
        [Required]
        [StringLength(50)]
        public string City { get; set; }
                
        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }
                
        public string Country { get; set; }

        public string State { get; set; }
        
    }
}