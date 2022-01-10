using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
     [Table("ReportedIssue")]
    public class ReportedIssueDBEntity : BaseDBEntity
    {   
        public ReportedIssueDBEntity()
        { }

        [Required]
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }

        [Required]
        [ForeignKey("IssueStatus"), Column("IssueStatusId")]
        public int IssueStatusId { get; set; }

        public virtual IssueStatusDBEntity IssueStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
                
        [StringLength(500)]
        public string Description { get; set; }

        
    }
}
