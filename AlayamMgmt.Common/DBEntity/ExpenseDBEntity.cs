using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
     [Table("Expense")]
    public class ExpenseDBEntity : BaseDBEntity
    {
        [Required]
        [ForeignKey("ExpenseType"), Column("ExpenseTypeId")]
        public int ExpenseTypeId { get; set; }

        [Required]
        [ForeignKey("SubExpenseType"), Column("SubExpenseTypeId")]
        public int SubExpenseTypeId { get; set; }

        
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int? OrganizationId { get; set; }

        [ForeignKey("Branch"), Column("BranchId")]
        public int? BranchId { get; set; }
       

        [Required]
        [ForeignKey("PaymentType"), Column("PaymentTypeId")]
        public int PaymentTypeId { get; set; }


        [Required]
        [ForeignKey("Account"), Column("AccountId")]
        public int AccountId { get; set; }

        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }

        public string TransactionNumber { get; set; }

        public string Notes { get; set; }

        public virtual AccountDBEntity Account { get; set; }
        public virtual ExpenseTypeDBEntity ExpenseType { get; set; }
        public virtual SubExpenseTypeDBEntity SubExpenseType { get; set; }        
        public virtual OrganizationDBEntity Organization { get; set; }
        public virtual OrganizationDBEntity Branch { get; set; }
        public virtual PaymentTypeDBEntity PaymentType { get; set; }
    }
}
