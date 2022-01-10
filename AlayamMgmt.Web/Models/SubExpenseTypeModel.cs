using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class SubExpenseTypeModel : OrganizationLookupModel
    {
       public SubExpenseTypeModel()
            : base()
       {
        
       }
        
        public int ExpenseTypeId { get; set; }

        public string ExpenseType { get; set; }
    }
}