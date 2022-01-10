using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class ExpensePrintModel : PrintBaseModel
    {        
        public decimal TotalExpese { get; set; }

        public ExpenseReportFilter Filter { get; set; }

        public List<Summary> SummaryByPaymentTypes { get; set; }

        public List<Summary> SummaryByAccounts { get; set; }

        public List<Summary> summaryByExpenseTypes { get; set; }

        public List<ExpenseModel> Expenses { get; set; }        
    }

    public class Summary
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
    }

    public class ExpenseReportFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public string ExpenseType { get; set; }

        public string Account { get; set; }

        public string OrderBy { get; set; }
    }
}