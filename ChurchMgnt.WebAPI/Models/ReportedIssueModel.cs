using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class ReportedIssueModel : BaseModel
    {
        public ReportedIssueModel()
        { }

        [Required]       
        public int OrganizationId { get; set; }

        public string Organization { get; set; }

        [Required]       
        public int IssueStatusId { get; set; }

        public string IssueStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
                
        [StringLength(500)]
        public string Description { get; set; }
    }
}