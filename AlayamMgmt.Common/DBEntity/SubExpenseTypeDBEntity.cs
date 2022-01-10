using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("SubExpenseType")]
    public class SubExpenseTypeDBEntity : LookupDBEntity
    {
        public SubExpenseTypeDBEntity()
            : base()
       {
        
       }


        [ForeignKey("ExpenseType"), Column("ExpenseTypeId")]
        public int ExpenseTypeId { get; set; }

        public virtual ExpenseTypeDBEntity ExpenseType { get; set; }
    }
}
