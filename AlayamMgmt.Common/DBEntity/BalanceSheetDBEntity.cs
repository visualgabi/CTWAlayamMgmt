using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class BalanceSheetDBEntity
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public int OrderId { get; set; }          
    }

    public class AccountBalanceSheetDBEntity : BalanceSheetDBEntity
    {
        public string Account { get; set; }        
    }

    public class QuarterBalanceSheetDBEntity : BalanceSheetDBEntity
    {
        public string Quarter { get; set; }        
    }
}
