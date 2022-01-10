using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class TransactionMonthlyDBEntity
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Type { get; set; }
    }
}
