using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/FamilyMember")]
    public class FamilyMemberController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.Get(active),true);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemebrBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                FamilyMemberModel familyMemberModel = GetFamilyMemberModel(familyMemebrBusiness.GetById(id), true);

                response = Request.CreateResponse(HttpStatusCode.OK, familyMemberModel);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

       // [CustomAuthorizeAttribute]
        [Route("GetByFamilyId")]
        [HttpGet]
        public HttpResponseMessage GetByFamilyId(int id, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

				//IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
				IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness("gabisusila@gmail.com");
				List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetFamilyMembersIncludeGroup(id, active),true);
				string str = JsonConvert.SerializeObject(familymemberModels);
                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetGroupFamilyMembersByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetGroupFamilyMembersByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetGroupFamilyMembersByOrgId(orgId), false);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetGroupFamilyMembers");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetGroupFamilyMembersByOrgIdIncludeGroups")]
        [HttpGet]
        public HttpResponseMessage GetGroupFamilyMembersByOrgIdIncludeGroups(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetGroupFamilyMembersByOrgId(orgId), true);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetGroupFamilyMembers");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetByFamilyIdAndGreaterThanAge")]
        [HttpGet]
        public HttpResponseMessage GetByFamilyIdAndGreaterThanAge(int familyId, int age)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetByFamilyIdAndGreaterThanAge(familyId, age), true);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [CustomAuthorizeAttribute]
        [Route("GetTaxPayerByFamilyId")]
        [HttpGet]
        public HttpResponseMessage GetTaxPayerByFamilyId(int familyId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetTaxPayerByFamilyId(familyId, active), false);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetTaxPayerByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetTaxPayerByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetTaxPayerByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetTaxPayerByOrgId(orgId, active),false);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

      

        [CustomAuthorizeAttribute]
        [Route("GetByGreaterThanAge")]
        [HttpGet]
        public HttpResponseMessage GetByGreaterThanAge(int age)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                List<FamilyMemberModel> familymemberModels = GetFamilyMemberModels(familyMemberBusiness.GetByGreaterThanAge(age), false);

                response = Request.CreateResponse(HttpStatusCode.OK, familymemberModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        } 


        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]FamilyMemberModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                familyMemberBusiness.Save(GetFamilyMemberDBEntity(model));

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Save");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (DbUpdateException ex)
            {
                if (null != ex.InnerException || ex.InnerException.InnerException != null)
                {
                    var innerException = ex.InnerException.InnerException
                                               as System.Data.SqlClient.SqlException;
                    if (innerException != null &&
                           (
                               innerException.Number == 2627 ||
                               innerException.Number == 2601)
                           )
                    {
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.FirstName), this.GetType(), "Save");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }

        }

        [CustomAuthorizeAttribute]
        [Route("MoveFamilyMember")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]MoveMemberModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                familyMemberBusiness.MoveFamilyMember(model.FamilyMemberId,model.FamilyId, model.RelationshipId);

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "MoveFamilyMember");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "MoveFamilyMember");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (DbUpdateException ex)
            {
                if (null != ex.InnerException || ex.InnerException.InnerException != null)
                {
                    var innerException = ex.InnerException.InnerException
                                               as System.Data.SqlClient.SqlException;
                    if (innerException != null &&
                           (
                               innerException.Number == 2627 ||
                               innerException.Number == 2601)
                           )
                    {
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.FamilyMemberId.ToString()), this.GetType(), "MoveFamilyMember");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "MoveFamilyMember");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }

        }

        [CustomAuthorizeAttribute]
        [Route("Enable")]
        [HttpPost]
        public HttpResponseMessage Enable([FromBody] FamilyModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyMemberBusiness familyMemberBusiness = new FamilyMemberBusiness(claimModel.LoginUserEmail);
                byte[] returnValue = familyMemberBusiness.Enable(enableModel.Id, enableModel.Active);

                response = Request.CreateResponse(HttpStatusCode.OK, returnValue);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Enable");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Enable");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Enable");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }
    }
        
}
