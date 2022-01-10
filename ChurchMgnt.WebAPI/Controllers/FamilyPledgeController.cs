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
     [RoutePrefix("api/FamilyPledge")]
    public class FamilyPledgeController : BaseApiController
     {  
        
        [HttpGet]
         [CustomAuthorizeAttribute]
         [Route("Get")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyPledgeBusiness familyPledgeBusiness = new FamilyPledgeBusiness(claimModel.LoginUserEmail);
                List<FamilyPledgeModel> FamilyPledgeModels = GetFamilyPledgeModels(familyPledgeBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, FamilyPledgeModels);
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

                IFamilyPledgeBusiness familyPledgeBusiness = new FamilyPledgeBusiness(claimModel.LoginUserEmail);
                List<FamilyPledgeModel> FamilyPledgeModels = GetFamilyPledgeModels(familyPledgeBusiness.GetPledgesByOrgId(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, FamilyPledgeModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [HttpGet]
        [CustomAuthorizeAttribute]
        [Route("GetByFamilyPledgeByFiscalYearId")]
        public HttpResponseMessage GetByFamilyPledgeByFiscalYearId(int familyId, int fiscalYearId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyPledgeBusiness familyPledgeBusiness = new FamilyPledgeBusiness(claimModel.LoginUserEmail);
                List<FamilyPledgeModel> FamilyPledgeModels = GetFamilyPledgeModels(familyPledgeBusiness.GetPledgesByFamilyIdAndOrgFiscalYearId(familyId, fiscalYearId));

                response = Request.CreateResponse(HttpStatusCode.OK, FamilyPledgeModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetByFamilyPledgeByFiscalYearId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }


        [HttpGet]
        [CustomAuthorizeAttribute]
        [Route("GetByFamilyId")]
        public HttpResponseMessage GetByFamilyId(int familyId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IFamilyPledgeBusiness familyPledgeBusiness = new FamilyPledgeBusiness(claimModel.LoginUserEmail);
                List<FamilyPledgeModel> FamilyPledgeModels = GetFamilyPledgeModels(familyPledgeBusiness.GetPledgesByFamilyId(familyId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, FamilyPledgeModels);
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
        public HttpResponseMessage Save([FromBody]FamilyPledgeModel model)
        {
            HttpResponseMessage response;

            try
            {
                IFamilyPledgeBusiness familyPledgeBusiness = new FamilyPledgeBusiness(Constant.DEFAULT_EMAIL);
                familyPledgeBusiness.Save(GetFamilyPledgeDBEntity(model));

                response = Request.CreateResponse(HttpStatusCode.OK);
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
        public HttpResponseMessage Enable([FromBody] EnableModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                IFamilyPledgeBusiness familyPledgeBusiness = new FamilyPledgeBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = familyPledgeBusiness.Enable(enableModel.Id, enableModel.Active);

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


        private List<FamilyPledgeModel> GetFamilyPledgeModels(List<FamilyPledgeDBEntity> entities)
        {
            List<FamilyPledgeModel> models = new List<FamilyPledgeModel>();

            foreach (FamilyPledgeDBEntity entity in entities)
            {
                models.Add(GetFamilyPledgeModel(entity));
            }

            return models;
        }

        private FamilyPledgeModel GetFamilyPledgeModel(FamilyPledgeDBEntity entity)
        {
            return new FamilyPledgeModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,                
                RowTimeStamp = entity.RowTimeStamp,
                Id = entity.Id,
                Amount = entity.Amount,
                FamilyId = entity.FamilyId,
                Family = entity.Family.Name,
                FundTypeId = entity.FundTypeId,
                FundType = entity.FundType.Name,
                OrgFiscalYearId = entity.OrgFiscalYearId,
                OrgFiscalYear = entity.OrgFiscalYear.FiscalYear.Name
            };
        }

        private FamilyPledgeDBEntity GetFamilyPledgeDBEntity(FamilyPledgeModel model)
        {
            return new FamilyPledgeDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,                
                RowTimeStamp = model.RowTimeStamp,
                Amount = model.Amount,
                FamilyId = model.FamilyId,
                FundTypeId = model.FundTypeId,
                OrgFiscalYearId = model.OrgFiscalYearId,
                Id = model.Id
            };
        }
    }
}
