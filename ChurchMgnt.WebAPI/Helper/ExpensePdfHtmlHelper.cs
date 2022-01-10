using AlayamMgmt.Common;
using ChurchMgnt.WebAPI.Interface;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ChurchMgnt.WebAPI.Helper
{
    public class ExpensePdfHtmlHelper : IPdfHtmlHelper
    {
        public StringBuilder GenerateBody(PrintBaseModel printBaseModel)
        {
            ExpensePrintModel model = (ExpensePrintModel) printBaseModel;

            int currentPage = 1;            
            int extraPage = model.Expenses.Count() % 25 == 0 ? 1 : 2;
            int totalPage = (model.Expenses.Count() / 25) + extraPage;

            StringBuilder expensePrintHtml = new StringBuilder(Constant.EXPENSE_HTML);
            
            expensePrintHtml.Replace("[orgName]", model.OrgName);            
            expensePrintHtml.Replace("[currency]", model.Currency);            
            expensePrintHtml.Replace("[totalExpense]", model.TotalExpese.ToString());
            expensePrintHtml.Replace("[totalPage]", totalPage.ToString());

            StringBuilder filter = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Expense Start Date</th><td>" 
                + "[startDate]</td></tr><tr><th>Expense End Date</th><td>[endDate]</td></tr><tr><th>Account</th><td>[account]</td></tr><tr><th>Expense Type</th><td>[expenseType]</td></tr><tr><th>Order By</th><td>[oderBy]</td></tr></table>");

            filter.Replace("[startDate]", model.Filter.StartDate.ToShortDateString());
            filter.Replace("[endDate]", model.Filter.EndDate.ToShortDateString());
            filter.Replace("[account]", model.Filter.Account);
            filter.Replace("[expenseType]", model.Filter.ExpenseType);
            filter.Replace("[oderBy]", model.Filter.OrderBy);

            expensePrintHtml.Replace("[filter]", filter.ToString());

            StringBuilder summaryByPaymentType = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Payment Type</th><th>Total in [currency]</th></tr>");
            for (int iPos = 0; iPos < model.SummaryByPaymentTypes.Count; iPos++)
            {
                summaryByPaymentType.AppendLine("<tr><td>" + model.SummaryByPaymentTypes[iPos].Name + "</td><td>" + model.SummaryByPaymentTypes[iPos].Total.ToString() + "</td></tr>");
            }
            summaryByPaymentType.AppendLine("</table>");
            expensePrintHtml.Replace("[summaryByPaymentType]", summaryByPaymentType.ToString());


            StringBuilder summaryByAccount = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Account</th><th>Total in [currency]</th></tr>");
            for (int iPos = 0; iPos < model.SummaryByAccounts.Count; iPos++)
            {
                summaryByAccount.AppendLine("<tr><td>" + model.SummaryByAccounts[iPos].Name + "</td><td>" + model.SummaryByAccounts[iPos].Total.ToString() + "</td></tr>");
            }
            summaryByAccount.AppendLine("</table>");            
            expensePrintHtml.Replace("[summaryByAccount]", summaryByAccount.ToString());

            StringBuilder summaryByExpenseType = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Expense Type</th><th>Total in [currency]</th></tr>");
            for (int iPos = 0; iPos < model.summaryByExpenseTypes.Count; iPos++)
            {
                summaryByExpenseType.AppendLine("<tr><td>" + model.summaryByExpenseTypes[iPos].Name + "</td><td>" + model.summaryByExpenseTypes[iPos].Total.ToString() + "</td></tr>");
            }
            summaryByExpenseType.AppendLine("</table>");
            expensePrintHtml.Replace("[summaryByExpenseType]", summaryByExpenseType.ToString());
            //"<div style='page-break-before: always'><table id='tblExpense' class='table table-bordered table-striped'><tr><th> Expense Type</th><th>Sub Expense Type</th><th>Payment Type</th><th>Amount in [currency]</th><th>ExpenseDate</th><th>Account</th></tr>"
            StringBuilder expenseHmtl = new StringBuilder("");
            bool includeTableHeader = true;
            currentPage++;

            for (int iPos=0; iPos < model.Expenses.Count; iPos++)
            {                
                if (includeTableHeader == true)
                {
                    includeTableHeader = false;
                    expenseHmtl.AppendLine("<div style='page-break-before: always'><div class='row'><div class='col-md-12'>&nbsp;</div><div class='col-md-12'>&nbsp;</div></div><div class='row'><div class='form-group'><div class='col-md-12'><table style = 'width:100%'><tr>" +
                        "<td style = 'text-align: right'><b>Page :</b> [currentPage] of [totalPage]</td></tr></table></div></div></div> <div class='col-md-12'>&nbsp;</div><hr /><div class='col-md-12'>&nbsp;</div><table id='tblExpense' class='table table-bordered table-striped'><tr><th> Expense Type</th><th>Sub Expense Type</th><th>Payment Type</th><th>Amount in [currency]</th><th>ExpenseDate</th><th>Account</th></tr>");
                    expenseHmtl.Replace("[currentPage]", currentPage.ToString());
                    expenseHmtl.Replace("[totalPage]", totalPage.ToString());
                }

                expenseHmtl.AppendLine("<tr><td>[expenseType]</td><td>[subExpenseType]</td><td>[paymentType]</td><td>[amount]</td><td>[expenseDate]</td><td>[account]</td></tr>");
                expenseHmtl.Replace("[expenseType]", model.Expenses[iPos].ExpenseType);
                expenseHmtl.Replace("[subExpenseType]", model.Expenses[iPos].SubExpenseType);
                expenseHmtl.Replace("[paymentType]", model.Expenses[iPos].PaymentType);
                expenseHmtl.Replace("[amount]", model.Expenses[iPos].Amount.ToString());
                expenseHmtl.Replace("[expenseDate]", model.Expenses[iPos].ExpenseDate.ToShortDateString());
                expenseHmtl.Replace("[account]", model.Expenses[iPos].Account.ToString());

                if (iPos != 0 && iPos % 25 == 0)
                {
                    currentPage++;
                    includeTableHeader = true;
                    expenseHmtl.AppendLine("</table></div>");
                }
            }

            expenseHmtl.AppendLine("</table></div>");
            expensePrintHtml.Replace("[expenses]", expenseHmtl.ToString());

            expensePrintHtml.Replace("[currency]", model.Currency);
            return expensePrintHtml;

        }

        public StringBuilder GenerateFooter(PrintBaseModel printBaseModel)
        {

            StringBuilder footerHTML = new StringBuilder("<div class='col-md-12'><table style='width:100%'><tr><td colspan='3'><hr /></td></tr><tr><td style='text-align: left'>Expense report</td><td style='text-align: center'>&nbsp;" +
           "</td><td style='text-align: right'>Date: " + DateTime.Now.ToShortDateString() + "</td></tr></table></div>");

          //  footerHTML.Replace("[currentPage]", currentPage);
           // footerHTML.Replace("[totalPage]", totalPage);
          
            return footerHTML;
        }

    }
}