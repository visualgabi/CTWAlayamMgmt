using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class OrgFiscalYearModel : BaseModel
    {
        public int FiscalYearId { get; set; }

        public DateTime FiscalYearStart { get; set; }

        public DateTime FiscalYearEnd { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        public string Organization { get; set; }

        public string FiscalYear { get; set; }
    }
}