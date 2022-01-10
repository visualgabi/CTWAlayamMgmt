using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ChurchMgnt.WebAPI.Controllers
{
   // [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    [RoutePrefix("api/Contact")]
    public class ContactController : ApiController
    {
        [Route("SendMessage")]
        [HttpPost]
        public HttpResponseMessage SendMessage(ContactModel contactModel)
        {

            HttpResponseMessage response;

            try
            {
                emailNotification(contactModel);
                smsNotification(contactModel);

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;

            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, 1);
                return response;
            }

        }

        private void emailNotification(ContactModel contactModel)
        {
            string fromEmail = ConfigurationManager.AppSettings["FromEmailId"].ToString();
            string toEmail = ConfigurationManager.AppSettings["ToEmail"].ToString();
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            string smtpServerPort = ConfigurationManager.AppSettings["SMTPServerPort"].ToString();
            string fromEmailLabel = ConfigurationManager.AppSettings["FromEmailLabel"].ToString();

            string smtpServerUserName = ConfigurationManager.AppSettings["SMTPServerUserName"].ToString();
            string smtpServerPassword = ConfigurationManager.AppSettings["SMTPServerPassword"].ToString();

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(toEmail);

            mail.From = new MailAddress(fromEmail, fromEmailLabel, System.Text.Encoding.UTF8);
            mail.Subject = "Customer contacted through online for ChurchMgnt";

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = @"<p>Email Notification from online customer.</p>
                            <p></p>
                            <p></p>
                            <p>The following customer contact us with the following details..</p>
                            <p></p>
                            <p></p>
                            <table>	
	                            </tr><th align='right'>Customer Name :</th><td>" + contactModel.Name + @"</td></tr>
	                            </tr><th align='right'>Phone Number :</th><td>" + contactModel.Phone + @"</td></tr>
	                            </tr><th align='right'>Email Id :</th><td>" + contactModel.Email + @"</td></tr>
	                            </tr><th align='right'>Message :</th><td>" + contactModel.Message + @"</td></tr>
                            </table>
                            <p></p>
                            <p></p>
                            <p>Thanks,</p>
                            <p>ChurchMgnt support Center.</p>"; ;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;

            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(smtpServerUserName, smtpServerPassword);
            client.Port = int.Parse(smtpServerPort);
            client.Host = smtpServer;
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
        }

        private void smsNotification(ContactModel contactModel)
        {
            string fromEmail = ConfigurationManager.AppSettings["FromEmailId"].ToString();
            string toEmail = ConfigurationManager.AppSettings["SMSEMailToText"].ToString();
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            string smtpServerPort = ConfigurationManager.AppSettings["SMTPServerPort"].ToString();
            string fromEmailLabel = ConfigurationManager.AppSettings["FromEmailLabel"].ToString();

            string smtpServerUserName = ConfigurationManager.AppSettings["SMTPServerUserName"].ToString();
            string smtpServerPassword = ConfigurationManager.AppSettings["SMTPServerPassword"].ToString();

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(toEmail);

            mail.From = new MailAddress(fromEmail, fromEmailLabel, System.Text.Encoding.UTF8);
            mail.Subject = "Msg from web.churchmgnt.com";

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Customer Name: " + contactModel.Name + " Phone: " + contactModel.Phone + " Msg:" + contactModel.Message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;

            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(smtpServerUserName, smtpServerPassword);
            client.Port = int.Parse(smtpServerPort);
            client.Host = smtpServer;
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
        }
    }
}
