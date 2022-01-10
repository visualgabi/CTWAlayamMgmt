using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    
    [Table("OrgFiscalYear")]
    public class OrgFiscalYearDBEntity : BaseDBEntity
    {
        public OrgFiscalYearDBEntity()
        {
           // Organizations = new HashSet<OrganizationDBEntity>();
        }

        [Required]
        [ForeignKey("FiscalYear"), Column("FiscalYearId")]
        public int FiscalYearId { get; set; }

        public DateTime FiscalYearStart { get; set; }

        public DateTime FiscalYearEnd { get; set; }

        [Required]
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }

        public virtual FiscalYearDBEntity FiscalYear { get; set; }
       //public virtual ICollection<OrganizationDBEntity> Organizations { get; set; } 

       
        
    }
}
