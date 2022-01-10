using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class FamilyMemberModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }
        public string Initial { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DOB { get; set; }

        public string Phone1 { get; set; }
        public string EmailId1 { get; set; }

        public string Notes { get; set; }
        
        public int FamilyId { get; set; }

        public string Family { get; set; }

        [Required]
        public bool IsTaxPayer { get; set; }

        [Required]        
        public int RelationshipId { get; set; }

        public virtual string Relationship { get; set; }

        public List<GroupMemberModel> GroupMembers { get; set; }
    }

     public class MoveMemberModel
     {
         [Required]
         public int FamilyMemberId { get; set; }

         [Required]
         public int FamilyId { get; set; }

         [Required]
         public int RelationshipId { get; set; }
     }
}