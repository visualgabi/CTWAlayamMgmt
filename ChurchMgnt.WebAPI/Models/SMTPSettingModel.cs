using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    
    public class SMTPSettingModel : BaseModel
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
        public int OrganizationId { get; set; }

        public string Organization { get; set; }
    }
}