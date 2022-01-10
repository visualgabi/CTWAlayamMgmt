using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class TransactionQuarterReportModel
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Quarter { get; set; }
        public string Type { get; set; }
    }
}