using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("PaymentType")]
    public class PaymentTypeDBEntity : LookupDBEntity
    {
       public PaymentTypeDBEntity()
            : base()
       {
        
       }   
    }
}
