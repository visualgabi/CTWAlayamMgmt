using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class OpeningBalanceModel : BaseModel
    {
        [Required]        
        public int OrgFiscalYearId { get; set; }

        [Required]        
        public int AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string OrgFiscalYear { get; set; }

        public string Account { get; set; }
    }
}