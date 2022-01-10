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
    [Table("FamilyMember")]
    public class FamilyMemberDBEntity : BaseDBEntity
    {
        public FamilyMemberDBEntity()
        {
            GroupMembers = new List<GroupMemberDBEntity>();            
        }

        [Required]
        [StringLength(100)]        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Initial { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }

        public string Phone1 { get; set; }
        public string EmailId1 { get; set; }

        public string Notes { get; set; }

        [Required]
        public bool IsTaxPayer { get; set; }

        [Required]
        [ForeignKey("Family"), Column("FamilyId")]
        public int FamilyId { get; set; }        

        public virtual FamilyDBEntity Family { get; set; }

        [Required]
        [ForeignKey("Relationship"), Column("RelationshipId")]
        public int RelationshipId { get; set; }

        public virtual RelationshipDBEntity Relationship { get; set; }

        public virtual List<GroupMemberDBEntity> GroupMembers { get; set; }

    }
}
