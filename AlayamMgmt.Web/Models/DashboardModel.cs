using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            MemberListCount = new List<int>();
            VisitorListCount = new List<int>();
            FundTypes = new List<string>();
            Budgets = new List<decimal>();
            ExpenseTypes = new List<string>();
            Expenses = new List<decimal>();
            Offerings = new List<decimal>();
        }

        public List<int> MemberListCount { get; set; }
        public List<int> VisitorListCount { get; set; }
        public List<String> FundTypes { get; set; }
        public List<decimal> Budgets { get; set; }
        public List<String> ExpenseTypes { get; set; }
        public List<decimal> Expenses { get; set; }
        public List<decimal> Offerings { get; set; }
    }

    public class ExpenseDataModel
    {
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
    }

    public class OfferingDataModel
    {
        public string FundType { get; set; }
        public decimal Amount { get; set; }
    }
}
