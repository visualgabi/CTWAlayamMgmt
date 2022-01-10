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
    public class ContributionPdfHtmlHelper : IPdfHtmlHelper
    {        

        public StringBuilder GenerateBody(PrintBaseModel printBaseModel)
        {
            ContributionPrintModel model = (ContributionPrintModel)printBaseModel;

            StringBuilder contributionPrintHtml = new StringBuilder(Constant.CONTRIBUTION_HTML);

            int totalPages = 1;
            int noOfRowPerPage = 23;

            if (model.Offerings.Count > noOfRowPerPage)
            {
                if (((model.Offerings.Count) % noOfRowPerPage) > 0)
                {
                    totalPages = (model.Offerings.Count) / noOfRowPerPage;
                    totalPages++;
                }
                else
                    totalPages = (model.Offerings.Count) / noOfRowPerPage;
            }


            StringBuilder logoHtml = new StringBuilder("<img style='width:100px;height:100px' src='" + model.OrgLogoLink + "' alt='profile-picture'>");
            if (string.IsNullOrWhiteSpace(model.OrgLogoLink) == false)
                contributionPrintHtml.Replace("[orgLogo]", logoHtml.ToString());
            else
                contributionPrintHtml.Replace("[orgLogo]", "&nbsp;");

            string signatureHtml = "<div class='col-md-12'><p>Thank you for your generous gifts last year!</p><div class='col-md-12'><br /><br /></div><div class='col-md-12'>" +
                "&nbsp;</div><p><b>Pastor</b> " + model.OrgPastorName + "</p></div>";

            if (model.OrgIncludeSignature == false)
                contributionPrintHtml.Replace("[signature]", signatureHtml);
            else
            {
                signatureHtml = "<div class='col-md-12'><p>Thank you for your generous gifts last year!</p><div class='col-md-12'><br /><img style = 'width:200px;height:100px'  src='" + model.OrgSignatureLink + "' alt='signature-picture'>" +
                                "</div><div class='col-md-12'>&nbsp;</div><p><b>Pastor</b> " + model.OrgPastorName + "</p></div>";
                contributionPrintHtml.Replace("[signature]", signatureHtml);
            }

            string offeringPage1Html = string.Empty;
            for (int iPos = 0; iPos < model.Offerings.Count; iPos++)
            {
                if (iPos >= noOfRowPerPage)
                    break;
                offeringPage1Html = offeringPage1Html + "<tr><td>" + model.Offerings[iPos].PaymentType + "</td><td>" + model.Offerings[iPos].Amount + "</td><td>" + model.Offerings[iPos].OfferingDate.ToShortDateString() + "</td><td>" + model.Offerings[iPos].RefNo + "</td></tr>";
            }
            contributionPrintHtml.Replace("[offeringPage1]", offeringPage1Html);

            bool loopNextSet = model.Offerings.Count > noOfRowPerPage;
            int startPos = noOfRowPerPage;
            string offeringOtherPageHtml = String.Empty;
            int currentPage = 1;
            while (loopNextSet)
            {
                int forLoop = 0;
                loopNextSet = false;

                for (int iPos = startPos; iPos < model.Offerings.Count; iPos++)
                {
                    if (forLoop == 0)
                    {
                        currentPage++;
                        offeringOtherPageHtml = offeringOtherPageHtml + "<div style='page-break-before: always'><div class='row'><div class='form-group'><div class='col-md-12'>&nbsp;</div></div></div><div class='row'><div class='form-group'><div class='col-md-12'>&nbsp;" +
                        "</div></div></div><div class='row'><div class='form-group'><div class='col-md-12'><table style='width:100%'><tr><td style='text-align:right'><b>Page :</b>" + currentPage.ToString() + "/" + totalPages.ToString() + "</td></tr></table>" +
                           "</div></div></div><div class='row'><div class='form-group'><div class='col-md-12'>&nbsp;</div></div></div><div class='row'><div class='form-group'><div class='col-md-12'><table class='table table-bordered table-striped'><tr><th>Payment Type</th><th>Amount</th>" +
                           "<th>Offering Date</th><th>Ref No</th></tr>";
                    }

                    forLoop++;
                    offeringOtherPageHtml = offeringOtherPageHtml + "<tr><td>" + model.Offerings[iPos].PaymentType + "</td><td>" + model.Offerings[iPos].Amount + "</td><td>" + model.Offerings[iPos].OfferingDate.ToShortDateString() + "</td><td>" + model.Offerings[iPos].RefNo + "</td></tr>";

                    if (forLoop == noOfRowPerPage)
                    {
                        loopNextSet = true;
                        offeringOtherPageHtml = offeringOtherPageHtml + "</table></div></div></div></div>";
                        contributionPrintHtml.Replace("offeringPage1", offeringPage1Html);
                        startPos = startPos + noOfRowPerPage;
                        break;
                    }
                }

                if (loopNextSet == false)
                {
                    offeringOtherPageHtml = offeringOtherPageHtml + "</table></div></div></div></div>";
                    contributionPrintHtml.Replace("[offeringPage1]", offeringPage1Html);
                }

            }
            contributionPrintHtml.Replace("[offeringOtherPages]", offeringOtherPageHtml);


            contributionPrintHtml.Replace("[orgName]", model.OrgName);
            contributionPrintHtml.Replace("[orgAddress1]", model.OrgAddress1);
            contributionPrintHtml.Replace("[orgCity]", model.OrgCity);
            contributionPrintHtml.Replace("[orgState]", model.OrgState);
            contributionPrintHtml.Replace("[orgZipCode]", model.OrgZipCode);
            contributionPrintHtml.Replace("[orgTaxId]", model.OrgTaxId);
            contributionPrintHtml.Replace("[printDate]", DateTime.Today.ToShortDateString());
            contributionPrintHtml.Replace("[reportStartDate]", model.StartDate.ToShortDateString());
            contributionPrintHtml.Replace("[reportEndDate]", model.EndDate.ToShortDateString());
            contributionPrintHtml.Replace("[donorName]", model.DonorName);
            contributionPrintHtml.Replace("[donorAddress1]", model.DonorAddress1);
            contributionPrintHtml.Replace("[donorCity]", model.DonorCity);
            contributionPrintHtml.Replace("[donorState]", model.DonorState);
            contributionPrintHtml.Replace("[donorZipCode]", model.DonorZipCode);
            contributionPrintHtml.Replace("[irsMsg]", model.OrgContributionMsg);
            contributionPrintHtml.Replace("[totalContribution]", model.TotalContribution);
            contributionPrintHtml.Replace("[totalPage]", totalPages.ToString());
            contributionPrintHtml.Replace("[currency]", model.Currency);


            return contributionPrintHtml;

        }

        public StringBuilder GenerateFooter(PrintBaseModel printBaseModel)
        {
            ContributionPrintModel model = (ContributionPrintModel)printBaseModel;

            StringBuilder footerHTML = new StringBuilder("<div class='col-md-12'><table style='width:100%'><tr><td colspan='3'><hr /></td></tr><tr><td style='text-align: left'>[orgWebsite]</td><td style='text-align: center'>[orgPhone]" +
           "</td><td style='text-align: right'>[orgEmailId]</td></tr></table></div>");

            footerHTML.Replace("[orgWebsite]", model.OrgWebsite);
            footerHTML.Replace("[orgPhone]", model.OrgPhone);
            footerHTML.Replace("[orgEmailId]", model.OrgEmail);

            return footerHTML;
        }
    }
}