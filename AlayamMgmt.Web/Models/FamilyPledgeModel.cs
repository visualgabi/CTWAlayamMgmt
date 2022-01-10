using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class FamilyPledgeModel : BaseModel
    {
        [Required]
        public int FamilyId { get; set; }

        [Required]
        public int OrgFiscalYearId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int FundTypeId { get; set; }

        public string Family { get; set; }

        public string FundType { get; set; }

        public string OrgFiscalYear { get; set; }
    }
}