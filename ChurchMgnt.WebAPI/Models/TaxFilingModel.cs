using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class TaxFilingModel : BaseModel
    {
        public TaxFilingModel()
        { }

        [Required]        
        public int OrgFiscalYearId { get; set; }

        public string OrgFiscalYear { get; set; }

        [Required]
        public DateTime FiledDate { get; set; }

        [Required]
        [StringLength(100)]
        public string FiledBy { get; set; }

        [Required]
        public decimal TotalRevenue { get; set; }

        [Required]
        public decimal TotalExpense { get; set; }

        [Required]
        public decimal TotalAsset { get; set; }

        [Required]
        public decimal TotalLiability { get; set; }

        [StringLength(250)]
        public string Notes { get; set; }
    }
}