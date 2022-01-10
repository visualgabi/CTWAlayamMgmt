using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class BalanceSheetModel
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int OrderId { get; set; }     
    }

    public class AccountBalanceSheetModel : BalanceSheetModel
    {
        public string Account { get; set; }        
    }

    public class QuarterBalanceSheetModel : BalanceSheetModel
    {
        public string Quarter { get; set; }
        
    }
}