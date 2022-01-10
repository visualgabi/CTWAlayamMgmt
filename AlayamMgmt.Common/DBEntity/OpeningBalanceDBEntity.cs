using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("OpeningBalance")]
    public class OpeningBalanceDBEntity : BaseDBEntity
    {
        [Required]
        [ForeignKey("OrgFiscalYear"), Column("OrgFiscalYearId")]
        public int OrgFiscalYearId { get; set; }

        [Required]
        [ForeignKey("Account"), Column("AccountId")]
        public int AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public virtual OrgFiscalYearDBEntity OrgFiscalYear { get; set; }

        public virtual AccountDBEntity Account { get; set; }
    }
}
