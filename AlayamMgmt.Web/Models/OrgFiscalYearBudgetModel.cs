using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class OrgFiscalYearBudgetModel : BaseModel
    {
        [Required]
        public int OrgFiscalYearId { get; set; }

        [Required]
        public int FundTypeId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string OrgFiscalYear { get; set; }

        public string FundType { get; set; }        
    }
}