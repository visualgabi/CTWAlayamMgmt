using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
     [Table("Deposit")]
    public class DepositDBEntity : BaseDBEntity
    {
        [Required]        
        public DateTime OfferingDate { get; set; }

        [Required]        
        public DateTime DepositDate { get; set; } 

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }


        [ForeignKey("User"), Column("UserId")]
        public int UserId { get; set; }

        [ForeignKey("Account"), Column("AccountId")]
        public int AccountId { get; set; }

        public virtual UserDBEntity User { get; set; }

        public virtual AccountDBEntity Account { get; set; }
    }
}
