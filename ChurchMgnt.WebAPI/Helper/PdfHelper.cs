using AlayamMgmt.Common;
using AlayamMgmt.Common.Helper;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Helper
{
    public static class PdfHelper
    {

        public static string Generate(string bodyHML, string footerHmtl, string webURL, string folderName, string orgName)
        {
            string fileName = string.Empty;
            try
            {
                string pdfDropLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"Files\" + orgName.Replace(" ","") + @"\Pdf\"+ folderName + @"\");

                fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";
                // string url = link;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.MinPageLoadTime = 2;
                GlobalProperties.CacheFonts = true;
                GlobalProperties.FontsFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"catching\fonts\");

                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = 50;

                PdfHtmlSection footerSection = new PdfHtmlSection(footerHmtl, webURL);
                footerSection.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Footer.Add(footerSection);

                PdfDocument doc = converter.ConvertHtmlString(bodyHML, webURL);

                doc.Save(fileName);
                doc.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, typeof(PdfHelper), "GenerateExpenseReportPdf");
                throw ex;
            }

            return fileName;
        }

        /*

        public static string GenerateContributionPdf(string bodyHML, string footerHmtl, string webURL)
        {
            string fileName = string.Empty;
            try
            {
                string pdfDropLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"pdf\");

                fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";
                // string url = link;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.MinPageLoadTime = 2;
                GlobalProperties.CacheFonts = true;
                GlobalProperties.FontsFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"catching\fonts\");

                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = 50;

                PdfHtmlSection footerSection = new PdfHtmlSection(footerHmtl, webURL);
                footerSection.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Footer.Add(footerSection);
                
                PdfDocument doc = converter.ConvertHtmlString(bodyHML, webURL);                

                doc.Save(fileName);
                doc.Close();                
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, typeof(PdfHelper), "DownloadPDFInLocal");
                throw ex;
            }

            return fileName;
        }

        public static string GenerateExpenseReportPdf(string bodyHML, string footerHmtl, string webURL)
        {
            string fileName = string.Empty;
            try
            {
                string pdfDropLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"pdf\");

                fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";
                // string url = link;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.MinPageLoadTime = 2;
                GlobalProperties.CacheFonts = true;
                GlobalProperties.FontsFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"catching\fonts\");

                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = 50;

                PdfHtmlSection footerSection = new PdfHtmlSection(footerHmtl, webURL);
                footerSection.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Footer.Add(footerSection);

                PdfDocument doc = converter.ConvertHtmlString(bodyHML, webURL);

                doc.Save(fileName);
                doc.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, typeof(PdfHelper), "GenerateExpenseReportPdf");
                throw ex;
            }

            return fileName;
        }

        public static string GenerateOfferingReportPdf(string bodyHML, string footerHmtl, string webURL)
        {
            string fileName = string.Empty;
            try
            {
                string pdfDropLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"pdf\");

                fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";
                // string url = link;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.MinPageLoadTime = 2;
                GlobalProperties.CacheFonts = true;
                GlobalProperties.FontsFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"catching\fonts\");

                converter.Options.DisplayFooter = true;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;
                converter.Footer.Height = 50;

                PdfHtmlSection footerSection = new PdfHtmlSection(footerHmtl, webURL);
                footerSection.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Footer.Add(footerSection);

                PdfDocument doc = converter.ConvertHtmlString(bodyHML, webURL);

                doc.Save(fileName);
                doc.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, typeof(PdfHelper), "GenerateExpenseReportPdf");
                throw ex;
            }

            return fileName;
        }
        */
    }
}