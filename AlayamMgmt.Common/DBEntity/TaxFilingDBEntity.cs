using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("TaxFiling")]
    public class TaxFilingDBEntity : BaseDBEntity
    {
        public TaxFilingDBEntity()
        { }

        [Required]
        [ForeignKey("OrgFiscalYear"), Column("OrgFiscalYearId")]
        public int OrgFiscalYearId { get; set; }

        public virtual OrgFiscalYearDBEntity OrgFiscalYear { get; set; }

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
