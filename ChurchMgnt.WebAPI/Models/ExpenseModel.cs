using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class ExpenseModel : BaseModel
    {
        [Required]        
        public int ExpenseTypeId { get; set; }

        [Required]        
        public int SubExpenseTypeId { get; set; }

        public int? OrganizationId { get; set; }

        public int? BranchId { get; set; }

        [Required]        
        public int PaymentTypeId { get; set; }

        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        public int AccountId { get; set; }

        public string TransactionNumber { get; set; }

        public string Notes { get; set; }
        public string ExpenseType { get; set; }
        public string SubExpenseType { get; set; }
        public string Organization { get; set; }
        public string Branch { get; set; }
        public string PaymentType { get; set; }
        
        public string Account { get; set; }


    }
}