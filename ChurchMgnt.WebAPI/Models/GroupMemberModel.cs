using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class GroupMemberModel : BaseModel
    {
        //public List<FamilyMemberModel> FamilyMembers { get; set; }

        public string FamilyMember  { get; set; }

        [Required]
        public int FamilyMemberId { get; set; }

        [Required]        
        public int GroupId { get; set; }

        public string Group { get; set; }
    }
}