using AlayamMgmt.Common;
using AlayamMgmt.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Upload")]
    public class UploadController : BaseApiController
    {
        [Route("Post")]
        [HttpPost]        
        [CustomAuthorizeAttribute]
        public void Post()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                ClaimModel claimModel = GetClaimModel();
                

                var httpPostedFile = HttpContext.Current.Request.Files["file"];

                int fileExtnPos = httpPostedFile.FileName.LastIndexOf(".");
                string currentDateTime = System.DateTime.Now.ToString("MMddyyyy HHmmss").Replace(" ", "");
                //string currentDateTime = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss").Replace(" ", "");
                //currentDateTime = currentDateTime.Replace(":", "");
                //currentDateTime = currentDateTime.Replace("-", "");

                string fileName = httpPostedFile.FileName.Substring(0, fileExtnPos) + currentDateTime + httpPostedFile.FileName.Substring(fileExtnPos);
                string folderName = claimModel.OrganizationName.Replace(" ", "");
                fileName = fileName.Replace(" ", "");

                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(@"~/Files/" + folderName + @"/Images/"));

                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(@"~/Files/" + folderName + @"/Images/"));

                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath(@"~/Files/" + folderName + @"/Images/"), fileName);
                httpPostedFile.SaveAs(fileSavePath);               
            }
            
        }

        [Route("Get")]
        [HttpGet]
        [CustomAuthorizeAttribute]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response;

            try
            {
                List<string> files = new List<string>();
                ClaimModel claimModel = GetClaimModel();
                string folderName = claimModel.OrganizationName.Replace(" ", "");
                string[] fileEntries = Directory.GetFiles(HttpContext.Current.Server.MapPath(@"~/Files/" + folderName + @"/Images/")).OrderByDescending(x => new FileInfo(x).CreationTime).ToArray();
                foreach (string fileName in fileEntries)
                    files.Add("Files/" + folderName + @"/Images/" + fileName.Substring(fileEntries[0].LastIndexOf(@"\")+1));

                response = Request.CreateResponse(HttpStatusCode.OK, files);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }

        }
    }



}
