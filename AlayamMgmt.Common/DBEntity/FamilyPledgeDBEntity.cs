using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
     [Table("FamilyPledge")]
    public class FamilyPledgeDBEntity : BaseDBEntity
    {
        [Required]
        [ForeignKey("Family"), Column("FamilyId")]
        public int FamilyId { get; set; }

        [Required]
        [ForeignKey("OrgFiscalYear"), Column("OrgFiscalYearId")]
        public int OrgFiscalYearId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [ForeignKey("FundType"), Column("FundTypeId")]
        public int FundTypeId { get; set; }

        public virtual FamilyDBEntity Family { get; set; }

        public virtual FundTypeDBEntity FundType { get; set; }

        public virtual OrgFiscalYearDBEntity OrgFiscalYear { get; set; }
    }
}
