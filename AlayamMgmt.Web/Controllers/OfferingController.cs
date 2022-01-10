using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Mapper;
using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/Offering")]
    public class OfferingController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetByOrgId(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        //[CustomAuthorizeAttribute]
        [Route("GetByFamilyId")]
        [HttpGet]
        public HttpResponseMessage GetByFamilyId(int familyId, DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetByFamilyId(familyId, startDate, endDate));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
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
        [Route("GetByFamilyMemberId")]
        [HttpGet]
        public HttpResponseMessage GetByFamilyMemberId(int familyMemberId, DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetByFamilyId(familyMemberId, startDate, endDate));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
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
        [Route("GetFiscalOfferingByFamilyId")]
        [HttpGet]
        public HttpResponseMessage GetFiscalOfferingByFamilyId(int familyId, int orgFiscalYearId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetFiscalOfferingByFamilyId(familyId, orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetFiscalOfferingByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetFiscalOfferingByFamilyMemberId")]
        [HttpGet]
        public HttpResponseMessage GetFiscalOfferingByFamilyMemberId(int familyMemberId, int orgFiscalYearId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetFiscalOfferingByFamilyMemberId(familyMemberId, orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetFiscalOfferingByFamilyMemberId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetFiscalOfferingBySponsorId")]
        [HttpGet]
        public HttpResponseMessage GetFiscalOfferingBySponsorId(int sponsorId, int orgFiscalYearId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetFiscalOfferingBySponsorId(sponsorId, orgFiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetFiscalOfferingBySponsorId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [CustomAuthorizeAttribute]
        [Route("GetRecentOfferingsByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetRecentOfferingsByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);

                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetRecentOfferingsByOrgId(orgId));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetRecentMembers");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("GetBySponsorId")]
        [HttpGet]
        public HttpResponseMessage GetBySponsorId(int sponsorId, DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                List<OrgOfferingModel> orgProfileModels = DBEntityToModel.GetOrgOfferingModels(orgOfferingBusiness.GetByFamilyId(sponsorId, startDate, endDate));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [Route("FamilyOfferingReport")]
        [HttpPost]
        public HttpResponseMessage FamilyOfferingReport(FamilyOfferingReportRequestModel searchModel)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness offeringBusiness = new OfferingBusiness(Constant.DEFAULT_EMAIL);
                List<OrgOfferingModel> OfferingModels = DBEntityToModel.GetOrgOfferingModels(offeringBusiness.FamilyOfferingReport(searchModel.FamilyId, searchModel.StartDate, searchModel.EndDate, searchModel.FundTypeId, searchModel.OfferingTypeId));

                response = Request.CreateResponse(HttpStatusCode.OK, OfferingModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Report");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }
        

        [Route("FamilyMemberOfferingReport")]
        [HttpPost]
        public HttpResponseMessage FamilyMemberOfferingReport(FamilyMemberOfferingReportRequestModel searchModel)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness offeringBusiness = new OfferingBusiness(Constant.DEFAULT_EMAIL);
                List<OrgOfferingModel> OfferingModels = DBEntityToModel.GetOrgOfferingModels(offeringBusiness.FamilyMemberOfferingReport(searchModel.FamilyMemberId, searchModel.StartDate, searchModel.EndDate, searchModel.FundTypeId, searchModel.OfferingTypeId));

                response = Request.CreateResponse(HttpStatusCode.OK, OfferingModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "FamilyMemberOfferingReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [Route("SponsorOfferingReport")]
        [HttpPost]
        public HttpResponseMessage SponsorOfferingReport(SponsorOfferingReportRequestModel searchModel)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness offeringBusiness = new OfferingBusiness(Constant.DEFAULT_EMAIL);
                List<OrgOfferingModel> OfferingModels = DBEntityToModel.GetOrgOfferingModels(offeringBusiness.SponsorMemberOfferingReport(searchModel.SponsorId, searchModel.StartDate, searchModel.EndDate, searchModel.FundTypeId, searchModel.OfferingTypeId));

                response = Request.CreateResponse(HttpStatusCode.OK, OfferingModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "SponsorOfferingReport");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        //[CustomAuthorizeAttribute]
        [Route("Report")]
        [HttpPost]
        public HttpResponseMessage Report(OfferingReportRequestModel searchModel)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                IOfferingBusiness offeringBusiness = new OfferingBusiness(Constant.DEFAULT_EMAIL);
                List<OrgOfferingModel> OfferingModels = DBEntityToModel.GetOrgOfferingModels(offeringBusiness.Report(searchModel.OrganizationId, searchModel.StartDate, searchModel.EndDate, searchModel.OrderById, searchModel.SourceId, searchModel.FundTypeId, searchModel.OfferingTypeId));

                response = Request.CreateResponse(HttpStatusCode.OK, OfferingModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Report");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Enable")]
        [HttpPost]
        public HttpResponseMessage Enable([FromBody] EnableModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();
                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                byte[] returnValue = orgOfferingBusiness.Enable(enableModel.Id, enableModel.Active);

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

        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]OrgOfferingModel model)
        {
            HttpResponseMessage response;

            try
            {
                ClaimModel claimModel = GetClaimModel();
                IOfferingBusiness orgOfferingBusiness = new OfferingBusiness(claimModel.LoginUserEmail);
                orgOfferingBusiness.Save(GetOfferingDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, "Offering"), this.GetType(), "Save");
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

        private OfferingDBEntity GetOfferingDBEntity(OrgOfferingModel model)
        {
            return new OfferingDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                OrganizationId = model.OrganizationId,
                RowTimeStamp = model.RowTimeStamp,
                Id = model.Id,
                Amount = model.Amount,
                FamilyMemberId = model.FamilyMemberId,
                SponsorId = model.SponsorId,
                FundTypeId = model.FundTypeId,
                OfferingDate = model.OfferingDate,
                OfferingTypeId = model.OfferingTypeId,
                Notes = model.Notes,
                PaymentTypeId = model.PaymentTypeId,                
                TransactionNumber = model.TransactionNumber
            };

        }
    }
}
