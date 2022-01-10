using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{    
    [Table("BirthdayRemainder")]
    public class BirthdayRemainderDBEntity : BaseDBEntity
    {
        [ForeignKey("FamilyMember"), Column("MemberId")]
        public int MemberId { get; set; }
        public virtual FamilyMemberDBEntity FamilyMember { get; set; }

        public DateTime BirthDate { get; set; }

        public bool WishedStatus { get; set; }

        [ForeignKey("User"), Column("WishedBy")]
        public int? WishedBy { get; set; }

        public virtual UserDBEntity User { get; set; }

        public DateTime? WishedDate { get; set; }

        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }
        public virtual OrganizationDBEntity Organization { get; set; }

    }
}
