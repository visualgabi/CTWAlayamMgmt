using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("MailMessage")]
    public class MailMessageDBEntity : BaseDBEntity
    {
        [Required]
        [StringLength(1000)]
        public string From { get; set; }

        [Required]
        [StringLength(1000)]
        public string To { get; set; }

        [Required]
        [StringLength(1000)]
        public string CC { get; set; }

        public int NoOfTries { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }       
    }
}
