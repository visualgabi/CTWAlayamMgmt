using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("Message")]
    public class MessageDBEntity : BaseDBEntity
    {
        [Required]
        [StringLength(4000)]
        public string Message { get; set; }

        [Required]
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }

        [Required]
        [ForeignKey("MessageType"), Column("MessageTypeId")]
        public int MessageTypeId { get; set; }

        public virtual MessageTypeDBEntity MessageType { get; set; }
    }
}
