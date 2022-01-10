using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class OrgProfileModel : BaseModel
    {
        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public string Currency { get; set; }

        public string Organization { get; set; }
    }
}