
using AlayamMgmt.Common;
using AlayamMgmt.Web.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/PDFConveter")]
    public class PDFConveterController : ApiController
    {
        [Route("DownloadPDF")]
        [HttpPost]
        public HttpResponseMessage DownloadPDF([FromBody]string link)
        {
            HttpResponseMessage response;
            try
            {
                string pdfDropLocation = ConfigurationManager.AppSettings["pdfDropLocation"].ToString();

                string fileName = pdfDropLocation + Guid.NewGuid().ToString() + ".pdf";
                string url = this.Request.RequestUri.AbsoluteUri.Substring(0, this.Request.RequestUri.AbsoluteUri.IndexOf("api")) + link;

                PdfConvert.ConvertHtmlToPdf(new PdfDocument
                {
                    Url = url

                }, new PdfOutput
                {
                    OutputFilePath = fileName
                });

                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(fileName, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = fileName;

                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }


        }
    }
}
