using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Helper;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/EmailCompose")]
    public class EmailComposeController : BaseApiController
    {
        [Route("ContributionLetterInEmail")]
        [HttpPost]
        [CustomAuthorizeAttribute]
        public HttpResponseMessage ContributionLetterInEmail([FromBody] ContributionPrintModel model)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();
                
                string htmlContent = new ContributionPdfHtmlHelper().GenerateBody(model).ToString();
                string footerContent = new ContributionPdfHtmlHelper().GenerateFooter(model).ToString();

                string pdfFileName = PdfHelper.Generate(htmlContent, footerContent, model.ChurchMgntURL, "Contribution", model.OrgName);

                string taxYear = model.EndDate.Year.ToString();
                string nextYear = (model.EndDate.Year + 1 ).ToString();
                string emailMsg = @"<p>Dear @DonorName</p> <p></p>                            
                            <table>
                                </tr><td>Thank you for your contribution towards @OrgName in @TaxYear.Last year''s donation receipt is attached.May the Lord increase you and bless you in @NextYear.</td></tr>     
                                 </table>     
                                 <p></p>     
                                 <p></p>     
                                 <p>Thanks,</p>     
                                 <p>@OrgName.</p>";

                IMessageBusiness business = new MessageBusiness(Constant.DEFAULT_EMAIL);
                MessageDBEntity messageEntity = business.getByOrgIdAndMsgTypeId(claimModel.OrgId.Value, (int)MessageType.ContributionEmailMsg);

                if(messageEntity != null && ! string.IsNullOrEmpty( messageEntity.Message ))
                {
                    emailMsg = messageEntity.Message;
                }

                emailMsg = string.Format(emailMsg, claimModel.OrganizationName, taxYear, nextYear);
                emailMsg = emailMsg.Replace("@OrgName", claimModel.OrganizationName);
                emailMsg = emailMsg.Replace("@TaxYear", taxYear);
                emailMsg = emailMsg.Replace("@NextYear", nextYear);
                emailMsg = emailMsg.Replace("@DonorName", model.DonorName);

                string subject = string.Format("{0} Contribution Report from {1}", taxYear, claimModel.OrganizationName);
               
                EmailHelper.ContributionLetterInEMail(claimModel.OrgId.Value, model.DonorEmailId, subject, emailMsg, pdfFileName);
                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, typeof(DownloadController), "DownloadPDFInLocal");

                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        /*
     [Route("SendEmail")]
     [HttpPost]
     [CustomAuthorizeAttribute]
     public void SendEmail(string to, HttpPostedFileBase file)
     {
         string str = "";
     }
     */

     [Route("SendEmail")]
     [HttpPost]
     [CustomAuthorizeAttribute]
    public void SendEmail()
    {
       // EmailComposeModel selectedModel = model;
    }
        
    } 


    public class EmailComposeModel
    {
        public EmailComposeModel()
        {
            Files = new List<string>();
            CustomizedGroups = new List<int>();
            OrganizationLevelGroups = new List<string>();
            BranchLevelGroups = new List<string>();
            FamilyMembers = new List<int>();
        }

        public string OrgName { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        public string CC { get; set; }
        public List<int> CustomizedGroups { get; set; }

        public List<string> OrganizationLevelGroups { get; set; }

        public List<string> BranchLevelGroups { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }

        public List<int> FamilyMembers { get; set; }
        public List<string> Files { get; set; }
    }

   
}
