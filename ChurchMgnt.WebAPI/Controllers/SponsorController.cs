using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Sponsor")]
    public class SponsorController : BaseApiController
    {
        [Authorize(Roles = "1")]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                ISponsorBusiness sponsorBusiness = new SponsorBusiness(claimModel.LoginUserEmail);
                List<SponsorModel> organizationModels = GetSponsorModels(sponsorBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, organizationModels);
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
        [Route("GetByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                ISponsorBusiness sponsorBusiness = new SponsorBusiness(claimModel.LoginUserEmail);
                List<SponsorModel> sponsorModels = GetSponsorModels(sponsorBusiness.GetByOrgId(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, sponsorModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        
        [Route("GetBasicDataById")]
        [HttpGet]
        public HttpResponseMessage GetBasicDataById(int id)
        {
            HttpResponseMessage response;
            try
            {

                ISponsorBusiness sponsorBusiness = new SponsorBusiness(Constant.DEFAULT_EMAIL);
                SponsorModel sponsorModel = GetSponsorBasicModel(sponsorBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, sponsorModel);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetBasicDataById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]SponsorModel model)
        {
            HttpResponseMessage response;

            try
            {
                ISponsorBusiness sponsorBusiness = new SponsorBusiness(Constant.DEFAULT_EMAIL);
                sponsorBusiness.Save(GetSponsorDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.Name), this.GetType(), "Save");
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
        [Route("IsUnique")]
        [HttpGet]
        public HttpResponseMessage IsUnique(int id, string name)
        {
            HttpResponseMessage response;
            try
            {
                ISponsorBusiness sponsorBusiness = new SponsorBusiness(Constant.DEFAULT_EMAIL);
                bool returnValue = sponsorBusiness.IsUnique(id, name);

                response = Request.CreateResponse(HttpStatusCode.OK, returnValue);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "IsUnique");
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
                ISponsorBusiness sponsorBusiness = new SponsorBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = sponsorBusiness.Enable(enableModel.Id, enableModel.Active);

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


        private List<SponsorModel> GetSponsorModels(List<SponsorDBEntity> SponsorDBEntities)
        {
            List<SponsorModel> SponsorModels = new List<SponsorModel>();

            foreach (SponsorDBEntity x in SponsorDBEntities)
            {
                SponsorModels.Add(GetSponsorModel(x));
            };

            return SponsorModels;

        }

        private SponsorModel GetSponsorModel(SponsorDBEntity entity)
        {
            return new SponsorModel()
            {
                Active = entity.Active,
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                Address3 = entity.Address3,
                City = entity.City,
                Country = entity.Country.Name,
                CountryId = entity.CountryId,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,                                
                EmailId1 = entity.EmailId1,                                
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                Name = entity.Name,
                Phone1 = entity.Phone1,                
                State = entity.State.Name,
                StateId = entity.StateId,
                Website = entity.Website,
                ZipCode = entity.ZipCode,
                RowTimeStamp = entity.RowTimeStamp,
                OrganizationId = entity.OrganizationId,
                Organization = entity.Organization.Name
            };
        }

        private SponsorModel GetSponsorBasicModel(SponsorDBEntity entity)
        {
            return new SponsorModel()
            {                
                Address1 = entity.Address1,             
                City = entity.City,
                Country = entity.Country.Name,
                EmailId1 = entity.EmailId1,
                Name = entity.Name,
                Phone1 = entity.Phone1,
                Website = entity.Website,
                State = entity.State.Name,
                ZipCode = entity.ZipCode,                
            };
        }

        private SponsorDBEntity GetSponsorDBEntity(SponsorModel model)
        {
            return new SponsorDBEntity()
            {
                Active = model.Active,
                Address1 = model.Address1,
                Address2 = model.Address2,
                Address3 = model.Address3,
                City = model.City,
                CountryId = model.CountryId,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,                                
                EmailId1 = model.EmailId1,                                
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                Name = model.Name,
                Phone1 = model.Phone1,                                
                StateId = model.StateId,
                Website = model.Website,
                ZipCode = model.ZipCode,
                RowTimeStamp = model.RowTimeStamp,
                OrganizationId = model.OrganizationId,
            };

        }    
    }
}
