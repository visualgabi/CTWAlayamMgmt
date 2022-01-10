using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Threading.Tasks;
using ChurchMgnt.WebAPI.Providers;
using System.Diagnostics;
using AlayamMgmt.Business;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.DBEntity;
using ChurchMgnt.WebAPI.Models;
using AlayamMgmt.Common;
using ChurchMgnt.WebAPI.Helper;
using System.IO;
using System.Net.Mail;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/FileUpload")]
    public class FileUploadController : BaseApiController
    {
       List<EmailWithMemberNameModel> EmailWithMemberNames = new List<EmailWithMemberNameModel>();
       int PKId = 0;

        [Route("upload")]
        public async Task<HttpResponseMessage> Post()
        {            
                ClaimModel claimModel = this.GetClaimModel();

                List<string> groupEmails = new List<string>();

                EmailComposeModel emailComposeModel = new EmailComposeModel();
                string folderName = claimModel.OrganizationName.Replace(" ", "");
                folderName = "~/Files/" + folderName + @"/EmailAttachement";


                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath(folderName);
                var provider = new CustomUploadMultipartFormProvider(root);
                try
                {
                    string emailGroup = string.Empty;
                List<Attachment> attachments = new List<Attachment>();
                    // Read the form data.
                    //await Request.Content.ReadAsMultipartAsync(provider);

                if (Request.Content.IsMimeMultipartContent())
                    {
                        await Request.Content.ReadAsMultipartAsync(provider).ContinueWith(t =>
                        {
                            if (t.IsFaulted || t.IsCanceled)
                                throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        });                   

                    // This illustrates how to get the file names.
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        emailComposeModel.Files.Add(file.LocalFileName);
                    }

                    foreach (var key in provider.FormData.AllKeys)
                    {
                        foreach (var val in provider.FormData.GetValues(key))
                        {
                            if (key == "from")
                                emailComposeModel.From = val;

                            if (key == "to")
                                emailComposeModel.To = val;

                            if (key == "cc")
                                emailComposeModel.CC = val;

                            if (key == "subject")
                                emailComposeModel.Subject = val;

                            if (key == "content")
                                emailComposeModel.Content = val;

                            if (key == "customizedGroups" && !string.IsNullOrEmpty(val))
                                emailComposeModel.CustomizedGroups = val.Split(',').Select(int.Parse).ToList();

                            if (key == "organizationLevelGroups" && !string.IsNullOrEmpty(val))
                                emailComposeModel.OrganizationLevelGroups = val.Split(',').ToList();

                            if (key == "branchLevelGroups" && !string.IsNullOrEmpty(val))
                                emailComposeModel.BranchLevelGroups = val.Split(',').ToList();

                            if (key == "familyMembers" && !string.IsNullOrEmpty(val))
                                emailComposeModel.FamilyMembers = val.Split(',').Select(int.Parse).ToList();

                        }
                    }

                    emailComposeModel.To = emailComposeModel.To.Replace(";", ",");

                    List<string> toEmailIds = emailComposeModel.To.Split(',').ToList();
                    toEmailIds.RemoveAll(x => !ValidationHelper.emailIsValid(x));

                    if (toEmailIds.Count > 0)
                    {
                        getToEmailMembers(toEmailIds, claimModel);
                    }

                    if (emailComposeModel.FamilyMembers.Count > 0)
                    {
                        getFamiliyMemberEmailIds(emailComposeModel.FamilyMembers, claimModel);
                    }                    

                    if (emailComposeModel.CustomizedGroups.Count > 0)
                    {
                        getCustomGroupEmailIds(emailComposeModel.CustomizedGroups, claimModel.LoginUserEmail);
                    }

                    if (emailComposeModel.BranchLevelGroups.Count > 0)
                    {
                        getBranchLevelGroupEmailIds(emailComposeModel.BranchLevelGroups, claimModel);
                    }

                    if (emailComposeModel.OrganizationLevelGroups.Count > 0)
                    {
                        getOrganizationLevelGroupEmailIds(emailComposeModel.OrganizationLevelGroups, claimModel);
                    }

                    EmailWithMemberNames.RemoveAll(x => !ValidationHelper.emailIsValid(x.EmailId));

                    EmailHelper.SendEmail(claimModel.OrgId.Value, emailComposeModel.From, toEmailIds, emailComposeModel.Subject, emailComposeModel.Content, emailComposeModel.Files, EmailWithMemberNames);


                    //Here you should return a meaningful response
                    return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                    }
                }
            
                catch (Exception e)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotImplemented)
                    {
                        Content = new StringContent(e.Message)
                    };
                }

        }

        /*
        [Route("upload")]
        public async Task<HttpResponseMessage> Post()
        {
            try
            {
                ClaimModel claimModel = this.GetClaimModel();

                List<string> groupEmails = new List<string>();               

                EmailComposeModel emailComposeModel = new EmailComposeModel();
                string folderName = claimModel.OrganizationName.Replace(" ", "");
                folderName = "~/Files/" + folderName + @"/EmailAttachement";             


                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }              

                string root = HttpContext.Current.Server.MapPath(folderName);
                var provider = new CustomUploadMultipartFormProvider(root);

                

                try
                {
                    string emailGroup=string.Empty;
                    // Read the form data.
                    await Request.Content.ReadAsMultipartAsync(provider);                      

                    // This illustrates how to get the file names.
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        emailComposeModel.Files.Add(file.LocalFileName);
                    }

                    

                    foreach (var key in provider.FormData.AllKeys)
                    {
                        foreach (var val in provider.FormData.GetValues(key))
                        {
                            if (key == "from")
                                emailComposeModel.From = val;

                            if (key == "to")
                                emailComposeModel.To = val;

                            if (key == "cc")
                                emailComposeModel.CC = val;

                            if (key == "subject")
                                emailComposeModel.Subject = val;

                            if (key == "content")
                                emailComposeModel.Content = val;

                            if (key == "customizedGroups" && ! string.IsNullOrEmpty(val))
                                emailComposeModel.CustomizedGroups = val.Split(',').Select(int.Parse).ToList();

                            if (key == "organizationLevelGroups" && !string.IsNullOrEmpty(val))
                                emailComposeModel.OrganizationLevelGroups = val.Split(',').ToList();

                            if (key == "branchLevelGroups" && !string.IsNullOrEmpty(val))
                                emailComposeModel.BranchLevelGroups = val.Split(',').ToList();

                            if (key == "familyMembers" && !string.IsNullOrEmpty(val))
                                emailComposeModel.FamilyMembers = val.Split(',').Select(int.Parse).ToList();

                        }
                    }

                    if( emailComposeModel.CustomizedGroups.Count > 0 )
                    {
                        getCustomGroupEmailIds(emailComposeModel.CustomizedGroups, claimModel.LoginUserEmail);
                    }

                    if (emailComposeModel.OrganizationLevelGroups.Count > 0)
                    {
                        getOrganizationLevelGroupEmailIds(emailComposeModel.OrganizationLevelGroups, claimModel);
                    }

                    if (emailComposeModel.BranchLevelGroups.Count > 0)
                    {
                        getBranchLevelGroupEmailIds(emailComposeModel.BranchLevelGroups, claimModel);
                    }

                    emailComposeModel.To = emailComposeModel.To.Replace(";", ",");

                    List<string> toEmailIds = emailComposeModel.To.Split(',').ToList();
                    toEmailIds.RemoveAll(x => !ValidationHelper.emailIsValid(x));

                    if (toEmailIds.Count > 0)
                    {
                        getToEmailMembers(toEmailIds, claimModel);
                    }                   

                    EmailWithMemberNames.RemoveAll(x => !ValidationHelper.emailIsValid(x.EmailId));                    

                    EmailHelper.SendEmail(claimModel.OrgId.Value, emailComposeModel.From, toEmailIds, emailComposeModel.Subject, emailComposeModel.Content, emailComposeModel.Files, EmailWithMemberNames);

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (System.Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(e.Message)
                };
            }

        }
        */

        private void getToEmailMembers(List<string> emailIds, ClaimModel claimModel)
        {
            IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
            List<FamilyMemberDBEntity> familyMemberEntities = familyMemberBusiness.GetFamilyMembersByEmailIds(emailIds);

            familyMemberEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));
        }

        public void getCustomGroupEmailIds(List<int> groupIds, string userId)
        {
            IGroupMemberBusiness groupMemberBusiness = new GroupMemberBusiness(userId);
            List<GroupMemberDBEntity> entities = groupMemberBusiness.GetByGroupIds(groupIds, true);

            List<string> emailIds = new List<string>();            

            entities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.FamilyMemberId, EmailId = x.FamilyMember.EmailId1, Name = x.FamilyMember.FirstName + " " + x.FamilyMember.LastName, Gender = x.FamilyMember.Gender }));
            EmailWithMemberNames.RemoveAll(x => !ValidationHelper.emailIsValid(x.EmailId));
            
        }

        public void getFamiliyMemberEmailIds(List<int> ids, ClaimModel claimModel)
        {
            IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
            List<FamilyMemberDBEntity> familyMemberEntities = familyMemberBusiness.GetFamilyMembersByIds(ids);          

            familyMemberEntities.ForEach(x => EmailWithMemberNames.Add( new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender}));
            EmailWithMemberNames.RemoveAll(x => !ValidationHelper.emailIsValid(x.EmailId));
                     
        }

        public void getOrganizationLevelGroupEmailIds(List<string> groupIds, ClaimModel claimModel)
        {
            List<string> emailIds = new List<string>();

            foreach (string id in groupIds)
            {
                int type =  int.Parse( id.Split('-')[1] );

                if (type == (int)BuildInEmailGroupType.Family)
                {
                    IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
                    List<FamilyMemberDBEntity> mensEntities = familyMemberBusiness.GetGroupFamilyMembersByOrgId(claimModel.OrgId.Value);
                    mensEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));                 
                    
                }
                else if (type == (int)BuildInEmailGroupType.Mens )
                {
                    IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
                    List<FamilyMemberDBEntity> mensEntities = familyMemberBusiness.GetMenGroupFamilyMembersByOrgId(claimModel.OrgId.Value);
                    mensEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));                    
                }
                else if (type == (int)BuildInEmailGroupType.Womens)
                {
                    IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
                    List<FamilyMemberDBEntity> womensEntities = familyMemberBusiness.GetWomenGroupFamilyMembersByOrgId(claimModel.OrgId.Value);
                   
                    womensEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));
                }

            }
            EmailWithMemberNames.RemoveAll(x => !ValidationHelper.emailIsValid(x.EmailId));            
        }

        public void getBranchLevelGroupEmailIds(List<string> groupIds, ClaimModel claimModel)
        {
            List<string> emailIds = new List<string>();

            foreach (string id in groupIds)
            {
                int branchId = int.Parse(id.Split('-')[1]);
                int type = int.Parse(id.Split('-')[2]);

                if (type == (int)BuildInEmailGroupType.Family)
                {
                    IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
                    List<FamilyMemberDBEntity> familyEntities = familyMemberBusiness.GetGroupFamilyMembersByBranchId(claimModel.OrgId.Value);
                    familyEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));
                    
                }
                else if (type == (int)BuildInEmailGroupType.Mens)
                {
                    IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
                    List<FamilyMemberDBEntity> mensEntities = familyMemberBusiness.GetMenGroupFamilyMembersByBranchId(branchId);
                    
                    mensEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));
                }
                else if (type == (int)BuildInEmailGroupType.Womens)
                {
                    IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(Constant.DEFAULT_EMAIL);
                    List<FamilyMemberDBEntity> womensEntities = familyMemberBusiness.GetWomenGroupFamilyMembersByBranchId(branchId);
                    
                    womensEntities.ForEach(x => EmailWithMemberNames.Add(new EmailWithMemberNameModel() { PKId = PKId++, Id = x.Id, EmailId = x.EmailId1, Name = x.FirstName + " " + x.LastName, Gender = x.Gender }));
                }
            }

            EmailWithMemberNames.RemoveAll(x => !ValidationHelper.emailIsValid(x.EmailId));
        }
    }

    
}
