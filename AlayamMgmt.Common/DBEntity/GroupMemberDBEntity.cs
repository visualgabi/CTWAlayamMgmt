using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("GroupMember")]
    public class GroupMemberDBEntity : BaseDBEntity
    {
        [Required]
        [ForeignKey("FamilyMember"), Column("FamilyMemberId")]
        public int FamilyMemberId { get; set; }

        //public virtual List<FamilyMemberDBEntity> FamilyMembers { get; set; }
        public virtual FamilyMemberDBEntity FamilyMember { get; set; }

        [Required]
        [ForeignKey("Group"), Column("GroupId")]
        public int GroupId { get; set; }

        public virtual GroupDBEntity Group { get; set; }
    }
}
