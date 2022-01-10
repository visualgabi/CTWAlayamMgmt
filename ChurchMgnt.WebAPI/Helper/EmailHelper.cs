using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ChurchMgnt.WebAPI.Helper
{
    public static class EmailHelper
    {
        public static void NotificationEmail(int orgId, string to, string subject, string emailBody, string fileName)
        {
            SMTPSettingModel smtpSettingModel = getSMTPSettingModel(orgId);

            string fromEmail = smtpSettingModel.FromEmailId;
            string toEmail = to;
            string smtpServer = smtpSettingModel.SMTPServer;
            string smtpServerPort = smtpSettingModel.SMTPServerPort;
            string fromEmailLabel = smtpSettingModel.FromEmailLabel;

            string smtpServerUserName = smtpSettingModel.SMTPServerUserName;
            string smtpServerPassword = smtpSettingModel.SMTPServerPassword;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(toEmail);

            mail.From = new MailAddress(fromEmail, fromEmailLabel, System.Text.Encoding.UTF8);
            mail.Subject = subject;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = emailBody;
            if(fileName != null)
                mail.Attachments.Add(new Attachment(fileName));

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
            finally
            {
                foreach (Attachment attachment in mail.Attachments)
                {
                    attachment.Dispose();
                }
                client.Dispose();
            }
        }      

        public static void ContributionLetterInEMail(int orgId, string to, string subject, string emailBody, string fileName)
        {
            SMTPSettingModel smtpSettingModel = getSMTPSettingModel(orgId);

            string fromEmail = smtpSettingModel.FromEmailId;
            string toEmail = to;
            string smtpServer = smtpSettingModel.SMTPServer;
            string smtpServerPort = smtpSettingModel.SMTPServerPort;
            string fromEmailLabel = smtpSettingModel.FromEmailLabel;

            string smtpServerUserName = smtpSettingModel.SMTPServerUserName;
            string smtpServerPassword = smtpSettingModel.SMTPServerPassword;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(toEmail);

            mail.From = new MailAddress(fromEmail, fromEmailLabel, System.Text.Encoding.UTF8);
            mail.Subject = subject;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = emailBody;
            mail.Attachments.Add(new Attachment(fileName));
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

        public static void SendEmail(int orgId, string from, List<string> tos, string subject, string emailBody, List<string> fileNames, List<EmailWithMemberNameModel> EmailWithMemberNames)
        {
            List<string> distinctEmails = EmailWithMemberNames.Select(x => x.EmailId).Distinct().ToList();

            SMTPSettingModel smtpSettingModel = getSMTPSettingModel(orgId);

            string smtpServer = smtpSettingModel.SMTPServer;
            string smtpServerPort = smtpSettingModel.SMTPServerPort;
            string fromEmailLabel = smtpSettingModel.FromEmailLabel;

            string smtpServerUserName = smtpSettingModel.SMTPServerUserName;
            string smtpServerPassword = smtpSettingModel.SMTPServerPassword;
                       

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(smtpServerUserName, smtpServerPassword);
            client.Port = int.Parse(smtpServerPort);
            client.Host = smtpServer;
            client.EnableSsl = true;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new MailAddress(from, fromEmailLabel, System.Text.Encoding.UTF8);

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Subject = subject;

            foreach (string fileName in fileNames)
            {
                mail.Attachments.Add(new Attachment(fileName));
            }
            mail.Priority = MailPriority.High;

            foreach (string to in tos)
            {
                string header = getEmailHeader(to, EmailWithMemberNames);

                string toEmailBody = string.Format(@"
                            <p>{0}</p>
                            <p></p>                            
                            <table>	
	                            </tr><td>{1}</td></tr>	                          
                            </table>", header, emailBody);

                mail.To.Clear();
                mail.To.Add(to);
                mail.Body = toEmailBody;
                send(client, mail);
            }
         
            foreach (EmailWithMemberNameModel familyMemberEmail in EmailWithMemberNames.OrderBy(x => x.PKId))
            {
                if ( ! tos.Contains(familyMemberEmail.EmailId)  && familyMemberEmail.EmailSend == false)
                {
                    string header = getEmailHeader(familyMemberEmail.EmailId, EmailWithMemberNames);

                    string groupEmailBody = string.Format(@"
                            <p>{0}</p>
                            <p></p>                            
                            <table>	
	                            </tr><td>{1}</td></tr>	                           
                            </table>", header, emailBody);

                    mail.To.Clear();
                    mail.To.Add(familyMemberEmail.EmailId);
                    mail.Body = groupEmailBody;

                    send(client, mail);
                }

                EmailWithMemberNames.Where(w => w.EmailId == familyMemberEmail.EmailId).ToList().ForEach(s => s.EmailSend = true);                
            }

            foreach (Attachment attachment in mail.Attachments)
            {
                attachment.Dispose();
            }
            client.Dispose();
        }

        private static string getEmailHeader(string emailId, List<EmailWithMemberNameModel> emailWithMemberNames)
        {            

            string header = ConfigurationManager.AppSettings["DefaultEmailHeader"].ToString();
            //find the family member data
            List<EmailWithMemberNameModel> filters = emailWithMemberNames.Where(x => x.EmailId == emailId).ToList();

            if (filters.Count == 0)
                filters = emailWithMemberNames.Where(x => x.EmailId == emailId).ToList();

            if (filters.Count > 0)
            {
                header = "Dear " + filters[0].Name + ",";
            }             

            return header;
        }       

        private static void send(SmtpClient client, MailMessage mail)
        { 
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                //Ignore the exception since we are logging the exception details.
                LogHelper.WriteToLog(Level.Error, string.Format("Unhandled exception occurred while sending email to {0}, subject : {1}, Email body: {2} ", mail.To, mail.Subject, mail.Body ));
                LogHelper.WriteToLog(Level.Error, ex, "EmailHelper","DownloadPDFInLocal");
            }            
        }

        private static SMTPSettingModel getSMTPSettingModel(int orgId)
        {
            IOrgSMTPDetailsBusiness business = new SMTPSettingBusiness(Constant.DEFAULT_EMAIL);
            return GetOrgSMTPDetailsModel( business.GetOrgId(orgId, true).FirstOrDefault() );
        }

        private static SMTPSettingModel GetOrgSMTPDetailsModel(SMTPSettingDBEntity entity)
        {
            if (entity != null)
                return new SMTPSettingModel()
                {
                    Active = entity.Active,
                    CreateDateTime = entity.CreateDateTime,
                    CreateUser = entity.CreateUser,
                    FromEmailId = entity.FromEmailId,
                    FromEmailLabel = entity.FromEmailLabel,
                    Id = entity.Id,
                    LastUpdateDateTime = entity.LastUpdateDateTime,
                    LastUpdateUser = entity.LastUpdateUser,
                    OrganizationId = entity.OrganizationId,
                    RowTimeStamp = entity.RowTimeStamp,
                    SMTPServer = entity.SMTPServer,
                    SMTPServerPassword = entity.SMTPServerPassword,
                    SMTPServerPort = entity.SMTPServerPort,
                    SMTPServerUserName = entity.SMTPServerUserName,
                    Organization = entity.Organization.Name
                };
            else
                return null;
        }
    }
}