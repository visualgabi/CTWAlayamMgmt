using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class MemberVisitorGraphModel
    {
        public List<int> MemberListCount { get; set; }
        public List<int> VisitorListCount { get; set; }

        public MemberVisitorGraphModel()
        {
            MemberListCount = new List<int>();
            VisitorListCount = new List<int>();
        }        
    }

    public class BudgetReceivedGraphModel
    {
        public List<int> MemberListCount { get; set; }
        public List<int> VisitorListCount { get; set; }

        public BudgetReceivedGraphModel()
        {
            MemberListCount = new List<int>();
            VisitorListCount = new List<int>();
        }
    }

    public class BudgetGraphModel
    {
        public List<BudgetDataModel> Budgets { get; set; }

        public BudgetGraphModel()
        {
            Budgets = new List<BudgetDataModel>();
        }
    }

    public class ExpenseGraphModel
    {
        public List<ExpenseDataModel> Expenses { get; set; }

        public ExpenseGraphModel()
        {
            Expenses = new List<ExpenseDataModel>();
        }
    }

    public class FundTypeOfferingGraphModel
    {
        public List<decimal> Offerings { get; set; }
        public List<String> FundTypes { get; set; }
        public List<decimal> Budgets { get; set; }

        public FundTypeOfferingGraphModel()
        {
            Offerings = new List<decimal>();
            FundTypes = new List<string>();
            Budgets = new List<decimal>();

        }
    }

    public class DashboardModel
    {
        public DashboardModel()
        {
            MemberListCount = new List<int>();
            VisitorListCount = new List<int>();
            FundTypes = new List<string>();
            Budgets = new List<decimal>();
            BudgetsForGraph = new List<BudgetDataModel>();
            ExpenseTypes = new List<string>();
            Expenses = new List<ExpenseDataModel>();
            Offerings = new List<decimal>();
        }

        public List<int> MemberListCount { get; set; }
        public List<int> VisitorListCount { get; set; }
        public List<String> FundTypes { get; set; }
        public List<BudgetDataModel> BudgetsForGraph { get; set; }
        public List<decimal> Budgets { get; set; }
        public List<String> ExpenseTypes { get; set; }
        public List<ExpenseDataModel> Expenses { get; set; }
        public List<decimal> Offerings { get; set; }
    }

    public class ExpenseDataModel
    {
        public string Label { get; set; }
        public decimal Value { get; set; }
        public string Color { get; set; }
    }

    public class OfferingDataModel
    {
        public string FundType { get; set; }
        public decimal Amount { get; set; }
    }

    public class BudgetDataModel
    {
        public decimal Value { get; set; }
        public string Label { get; set; }
        public string Color { get; set; }
        public string Highlight { get; set; }         
    }
}
