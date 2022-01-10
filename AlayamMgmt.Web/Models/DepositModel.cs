using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class DepositModel : BaseModel
    {
        [Required]        
        public DateTime OfferingDate { get; set; }

        [Required]        
        public DateTime DepositDate { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }
                
        public int UserId { get; set; }
                
        public int AccountId { get; set; }

        public string User { get; set; }

        public string Account { get; set; }

        public string Bank { get; set; }
    }
}