using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("OrgSMTPDetails")]
    public class SMTPSettingDBEntity : BaseDBEntity
    {
        [StringLength(256)]
        public string SMTPServer { get; set; }

        [StringLength(10)]
        public string SMTPServerPort { get; set; }

        [StringLength(256)]
        public string SMTPServerUserName { get; set; }

        [StringLength(256)]
        public string SMTPServerPassword { get; set; }

        [StringLength(256)]
        public string FromEmailId { get; set; }

        [StringLength(256)]
        public string FromEmailLabel { get; set; }

        [Required]
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }
    }
}
