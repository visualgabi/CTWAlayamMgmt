
using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Helper;
using ChurchMgnt.WebAPI.Interface;
using ChurchMgnt.WebAPI.Models;
using SelectPdf;
//using ChurchMgnt.Web.Helper;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;



namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Download")]
    public class DownloadController : ApiController
    {
        [Route("ContributionLetter")]
        [HttpPost]
        public HttpResponseMessage ContributionLetter([FromBody] ContributionPrintModel model)
        {
            return download(model, PrintReportType.Contribution);          
        }

        [Route("ExpenseReport")]
        [HttpPost]
        public HttpResponseMessage ExpenseReport([FromBody] ExpensePrintModel model)
        {
            return download(model, PrintReportType.Expense);
        }

        [Route("OfferingReport")]
        [HttpPost]
        public HttpResponseMessage OfferingReport([FromBody] OfferingPrintModel model)
        {
            return download(model, PrintReportType.Offering);
        }

        [Route("FamilyPledgeReport")]
        [HttpPost]
        public HttpResponseMessage FamilyPledgeReport([FromBody] FamilyPledgePrintModel model)
        {
            return download(model, PrintReportType.FamilyPledge);
        }

        public HttpResponseMessage download(PrintBaseModel model, PrintReportType printReportType)
        {
            HttpResponseMessage response;
            IPdfHtmlHelper pdfHtmlHelper = new ExpensePdfHtmlHelper();
            try
            {
                string htmlContent = string.Empty;
                string footerContent = string.Empty;
                switch(printReportType)
                {
                    case PrintReportType.Contribution:
                    {
                        pdfHtmlHelper = new ContributionPdfHtmlHelper();                        
                        break;
                    }
                    case PrintReportType.Expense:
                    {
                        pdfHtmlHelper = new ExpensePdfHtmlHelper();
                        break;
                    }
                    case PrintReportType.Offering:
                    {
                        pdfHtmlHelper = new OfferingPdfHtmlHelper();
                        break;
                    }
                    case PrintReportType.FamilyPledge:
                    {
                        pdfHtmlHelper = new FamilyPledgePdfHtmlHelper();
                        break;
                    }
                }


                htmlContent = pdfHtmlHelper.GenerateBody(model).ToString();
                footerContent = pdfHtmlHelper.GenerateFooter(model).ToString();

                string pdfFileName = PdfHelper.Generate(htmlContent, footerContent, model.ChurchMgntURL, printReportType.ToString(), model.OrgName);

                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(pdfFileName, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = pdfFileName;

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, typeof(DownloadController), "download");

                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }
        /*
      public HttpResponseMessage downloadContributionLetter(ContributionPrintModel model)
      {
          HttpResponseMessage response;
          try
          {
              string htmlContent = ContributionPdfHtmlHelper.GenerateHmtl(model).ToString();
              string footerContent = ContributionPdfHtmlHelper.GenerateFooter(model).ToString();

              string pdfFileName = PdfHelper.Generate(htmlContent, footerContent, model.ChurchMgntURL, "Contribution");             

              response = new HttpResponseMessage(HttpStatusCode.OK);
              response.Content = new StreamContent(new FileStream(pdfFileName, FileMode.Open, FileAccess.Read));
              response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
              response.Content.Headers.ContentDisposition.FileName = pdfFileName;

              return response;
          }
          catch (Exception ex)
          {
              LogHelper.WriteToLog(Level.Error, ex, typeof(DownloadController), "DownloadPDFInLocal");

              response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
              return response;
          }
      }

      public HttpResponseMessage downloadExpenseReport(ExpensePrintModel model)
      {
          HttpResponseMessage response;
          try
          {
              string htmlContent = ExpensePdfHtmlHelper.GenerateHmtl(model).ToString();
              string footerContent = ExpensePdfHtmlHelper.GenerateFooter().ToString();

              string pdfFileName = PdfHelper.Generate(htmlContent, footerContent, model.ChurchMgntURL, "Expense");

              response = new HttpResponseMessage(HttpStatusCode.OK);
              response.Content = new StreamContent(new FileStream(pdfFileName, FileMode.Open, FileAccess.Read));
              response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
              response.Content.Headers.ContentDisposition.FileName = pdfFileName;

              return response;
          }
          catch (Exception ex)
          {
              LogHelper.WriteToLog(Level.Error, ex, typeof(DownloadController), "downloadExpenseReport");

              response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
              return response;
          }
      }

      public HttpResponseMessage downloadOfferingReport(OfferingPrintModel model)
      {
          HttpResponseMessage response;
          try
          {
              string htmlContent = OfferingPdfHtmlHelper.GenerateHmtl(model).ToString();
              string footerContent = OfferingPdfHtmlHelper.GenerateFooter().ToString();

              string pdfFileName = PdfHelper.Generate(htmlContent, footerContent, model.ChurchMgntURL, "Offering");

              response = new HttpResponseMessage(HttpStatusCode.OK);
              response.Content = new StreamContent(new FileStream(pdfFileName, FileMode.Open, FileAccess.Read));
              response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
              response.Content.Headers.ContentDisposition.FileName = pdfFileName;

              return response;
          }
          catch (Exception ex)
          {
              LogHelper.WriteToLog(Level.Error, ex, typeof(DownloadController), "downloadExpenseReport");

              response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
              return response;
          }
      }


      private HttpResponseMessage DownloadPDFByAPI(string link)
      {
          HttpResponseMessage response;
          try
          {
              string pdfDropLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"pdf\");
              string fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";

              string apiKey = ConfigurationManager.AppSettings["pdfAPIKey"].ToString();
              string value = this.Request.RequestUri.AbsoluteUri.Substring(0, this.Request.RequestUri.AbsoluteUri.IndexOf("api")) + link;
              //string value = "http://www.churchmgnt.com/#/print/contributionPrint/13/1/2016-01-01T00:00:00/2016-12-31T00:00:00/1/Gabriel,Susila/1";
              string apiUrl = ConfigurationManager.AppSettings["pdfAPI"].ToString(); ;

              using (var client = new WebClient())
              {
                  // Build the conversion options 
                  NameValueCollection options = new NameValueCollection();
                  options.Add("apikey", apiKey);
                  options.Add("value", value);

                  // Call the API convert to a PDF
                  MemoryStream ms = new MemoryStream(client.UploadValues(apiUrl, options));
                  FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                  ms.WriteTo(file);
                  file.Close();
                  ms.Close();

              }

              response = new HttpResponseMessage(HttpStatusCode.OK);
              response.Content = new StreamContent(new FileStream(fileName, FileMode.Open, FileAccess.Read));
              response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
              response.Content.Headers.ContentDisposition.FileName = fileName;

              return response;

          }
          catch (Exception ex)
          {
              LogHelper.WriteToLog(Level.Error, ex, typeof(PDFConveterController), "ConvertHtmlToPdf");

              response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
              return response;
          }

      }

      public HttpResponseMessage DownloadPDFInLocal(string link)
      {
          HttpResponseMessage response;
          try
          {
              //string pdfDropLocation = ConfigurationManager.AppSettings["pdfDropLocation"].ToString();
              string pdfDropLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"pdf\");


              string fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";
              //string url = this.Request.RequestUri.AbsoluteUri.Substring(0, this.Request.RequestUri.AbsoluteUri.IndexOf("api")) + link;                
              string url = link;

              IOrganizationBusiness orgBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
              OrganizationDBEntity orgEntity = orgBusiness.GetById(int.Parse(link.Split('/')[10].ToString()));



              HtmlToPdf converter = new HtmlToPdf();
              converter.Options.MinPageLoadTime = 2;
              GlobalProperties.CacheFonts = true;
              GlobalProperties.FontsFolder = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"catching\fonts\");

              converter.Options.DisplayFooter = true;
              converter.Footer.DisplayOnFirstPage = true;
              converter.Footer.DisplayOnOddPages = true;
              converter.Footer.DisplayOnEvenPages = true;
              converter.Footer.Height = 50;

              string footerMsg = @"<table style='width:100%'>
          <tr>
              <td colspan='3'>
                  <hr />
              </td>
          </tr>
          <tr>
              <td style='text-align: left'>" + orgEntity.Name + "</td><td style='text-align: center'> Email: " + orgEntity.EmailId1 + "</td><td style='text-align: right'>Phone: " + orgEntity.Phone1 + "</td></tr></table>";

              PdfHtmlSection pageNumberFd = new PdfHtmlSection(footerMsg, null);

              converter.Footer.Add(pageNumberFd);

              PdfDocument doc = converter.ConvertUrl(url);

              doc.Save(fileName);

              doc.Close();

              LogHelper.WriteToLog(Level.Information, url, "", "");

              response = new HttpResponseMessage(HttpStatusCode.OK);
              response.Content = new StreamContent(new FileStream(fileName, FileMode.Open, FileAccess.Read));
              response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
              response.Content.Headers.ContentDisposition.FileName = fileName;

              return response;
          }
          catch (Exception ex)
          {
              LogHelper.WriteToLog(Level.Error, ex, typeof(PDFConveterController), "DownloadPDFInLocal");

              response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
              return response;
          }

      }
      */
    }
}
