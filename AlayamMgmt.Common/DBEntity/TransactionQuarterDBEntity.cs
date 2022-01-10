using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class TransactionQuarterDBEntity
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Quarter { get; set; }
        public string Type { get; set; }
    }
}
