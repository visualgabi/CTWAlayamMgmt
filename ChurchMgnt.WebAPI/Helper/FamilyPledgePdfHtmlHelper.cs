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
    public class FamilyPledgePdfHtmlHelper : IPdfHtmlHelper
    {
        public StringBuilder GenerateBody(PrintBaseModel printBaseModel)
        {
            FamilyPledgePrintModel model = (FamilyPledgePrintModel)printBaseModel;

            int currentPage = 1;
            int extraPage = model.Offerings.Count() % 25 == 0 ? 1 : 2;
            int totalPage = (model.Offerings.Count() / 25) + extraPage;

            StringBuilder offeringPrintHtml = new StringBuilder(Constant.FAMILY_PLEDGE_HTML);

            offeringPrintHtml.Replace("[orgName]", model.OrgName);
            offeringPrintHtml.Replace("[currency]", model.Currency);
            offeringPrintHtml.Replace("[totalOffering]", model.TotalOffering.ToString());
            offeringPrintHtml.Replace("[totalPage]", totalPage.ToString());

            StringBuilder filter = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Offering Start Date</th><td>"
                + "[startDate]</td></tr><tr><th>Offering End Date</th><td>[endDate]</td></tr></table>");

            filter.Replace("[startDate]", model.Filter.StartDate.ToShortDateString());
            filter.Replace("[endDate]", model.Filter.EndDate.ToShortDateString());            

            offeringPrintHtml.Replace("[filter]", filter.ToString());         


            StringBuilder summaryByPaymentType = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Payment Type</th><th>Total in [currency]</th></tr>");
            for (int iPos = 0; iPos < model.SummaryByPaymentTypes.Count; iPos++)
            {
                summaryByPaymentType.AppendLine("<tr><td>" + model.SummaryByPaymentTypes[iPos].Name + "</td><td>" + model.SummaryByPaymentTypes[iPos].Total.ToString() + "</td></tr>");
            }
            summaryByPaymentType.AppendLine("</table>");
            offeringPrintHtml.Replace("[summaryByPaymentType]", summaryByPaymentType.ToString());


            StringBuilder summaryByFundType = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Fund type</th><th>Total in [currency]</th></tr>");
            for (int iPos = 0; iPos < model.summaryByFundTypes.Count; iPos++)
            {
                summaryByFundType.AppendLine("<tr><td>" + model.summaryByFundTypes[iPos].Name + "</td><td>" + model.summaryByFundTypes[iPos].Total.ToString() + "</td></tr>");
            }
            summaryByFundType.AppendLine("</table>");
            offeringPrintHtml.Replace("[summaryByFundType]", summaryByFundType.ToString());

            StringBuilder summaryByOfferingType = new StringBuilder("<table class='table table-bordered table-striped'><tr><th>Expense Type</th><th>Total in [currency]</th></tr>");
            for (int iPos = 0; iPos < model.SummaryByOfferingTypes.Count; iPos++)
            {
                summaryByOfferingType.AppendLine("<tr><td>" + model.SummaryByOfferingTypes[iPos].Name + "</td><td>" + model.SummaryByOfferingTypes[iPos].Total.ToString() + "</td></tr>");
            }
            summaryByOfferingType.AppendLine("</table>");
            offeringPrintHtml.Replace("[summaryByOfferingType]", summaryByOfferingType.ToString());
            //"<div style='page-break-before: always'><table id='tblExpense' class='table table-bordered table-striped'><tr><th> Expense Type</th><th>Sub Expense Type</th><th>Payment Type</th><th>Amount in [currency]</th><th>ExpenseDate</th><th>Account</th></tr>"
            StringBuilder expenseHmtl = new StringBuilder("");
            bool includeTableHeader = true;
            currentPage++;

            for (int iPos = 0; iPos < model.Offerings.Count; iPos++)
            {
                if (includeTableHeader == true)
                {
                    includeTableHeader = false;
                    expenseHmtl.AppendLine("<div style='page-break-before: always'><div class='row'><div class='col-md-12'>&nbsp;</div><div class='col-md-12'>&nbsp;</div></div><div class='row'><div class='form-group'><div class='col-md-12'><table style = 'width:100%'><tr>" +
                        "<td style = 'text-align: right'><b>Page :</b> [currentPage] of [totalPage]</td></tr></table></div></div></div> <div class='col-md-12'>&nbsp;</div><hr /><div class='col-md-12'>&nbsp;</div><table id='tblExpense' class='table table-bordered table-striped'><tr><th>Offering Type</th><th>Offering Type</th><th>Payment Type</th><th>Amount in [currency]</th><th>OfferingDate</th></tr>");
                    expenseHmtl.Replace("[currentPage]", currentPage.ToString());
                    expenseHmtl.Replace("[totalPage]", totalPage.ToString());
                }

                expenseHmtl.AppendLine("<tr><td>[fundType]</td><td>[offeringType]</td><td>[paymentType]</td><td>[amount]</td><td>[offeringDate]</td></tr>");                
                expenseHmtl.Replace("[fundType]", model.Offerings[iPos].FundType);
                expenseHmtl.Replace("[offeringType]", model.Offerings[iPos].OfferingType);
                expenseHmtl.Replace("[paymentType]", model.Offerings[iPos].PaymentType);
                expenseHmtl.Replace("[amount]", model.Offerings[iPos].Amount.ToString());
                expenseHmtl.Replace("[offeringDate]", model.Offerings[iPos].OfferingDate.ToShortDateString());


                if (iPos != 0 && iPos % 25 == 0)
                {
                    currentPage++;
                    includeTableHeader = true;
                    expenseHmtl.AppendLine("</table></div>");
                }
            }

            expenseHmtl.AppendLine("</table></div>");
            offeringPrintHtml.Replace("[offerings]", expenseHmtl.ToString());

            offeringPrintHtml.Replace("[currency]", model.Currency);
            return offeringPrintHtml;

        }

        public StringBuilder GenerateFooter(PrintBaseModel printBaseModel)
        {

            StringBuilder footerHTML = new StringBuilder("<div class='col-md-12'><table style='width:100%'><tr><td colspan='3'><hr /></td></tr><tr><td style='text-align: left'>Family pledge report</td><td style='text-align: center'>&nbsp;" +
           "</td><td style='text-align: right'>Date: " + DateTime.Now.ToShortDateString() + "</td></tr></table></div>");

            //  footerHTML.Replace("[currentPage]", currentPage);
            // footerHTML.Replace("[totalPage]", totalPage);

            return footerHTML;
        }
    }
}