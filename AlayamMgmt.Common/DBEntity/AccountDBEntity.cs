using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
     [Table("Account")]
    public class AccountDBEntity : BaseDBEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Number { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [ForeignKey("AccountType"), Column("AccountTypeId")]
        public int AccountTypeId { get; set; }

        [ForeignKey("Bank"), Column("BankId")]
        public int BankId { get; set; }

        public virtual AccountTypeDBEntity AccountType { get; set; }

        public virtual BankDBEntity Bank { get; set; }
    }
}
