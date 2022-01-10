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
    [RoutePrefix("api/OrgFiscalYear")]
    public class OrgFiscalYearController : BaseApiController
    {
        //[CustomAuthorizeAttribute]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgFiscalYearBusiness orgFiscalYearBusiness = new OrgFiscalYearBusiness(claimModel.LoginUserEmail);
                List<OrgFiscalYearModel> orgFiscalYearModels = GetOrgFiscalYearModels(orgFiscalYearBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, orgFiscalYearModels);
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
        [Route("GetOrgFiscalYearByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetOrgFiscalYearByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgFiscalYearBusiness orgFiscalYearBusiness = new OrgFiscalYearBusiness(claimModel.LoginUserEmail);
                List<OrgFiscalYearModel> orgFiscalYearModels = GetOrgFiscalYearModels(orgFiscalYearBusiness.GetOrgFiscalYear(orgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, orgFiscalYearModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetProfileByOrgId");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        
        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]OrgFiscalYearModel model)
        {
            HttpResponseMessage response;

            try
            {
                IOrgFiscalYearBusiness orgFiscalYearBusiness = new OrgFiscalYearBusiness(Constant.DEFAULT_EMAIL);
                orgFiscalYearBusiness.Save(GetOrgFiscalYearDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.FiscalYear), this.GetType(), "Save");
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
        [Route("Enable")]
        [HttpPost]
        public HttpResponseMessage Enable([FromBody] EnableModel enableModel)
        {
            HttpResponseMessage response;
            try
            {
                IOrgFiscalYearBusiness orgFiscalYearBusiness = new OrgFiscalYearBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = orgFiscalYearBusiness.Enable(enableModel.Id, enableModel.Active);            

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

        private List<OrgFiscalYearModel> GetOrgFiscalYearModels(List<OrgFiscalYearDBEntity> entities)
        {
            List<OrgFiscalYearModel> models = new List<OrgFiscalYearModel>();

            foreach (OrgFiscalYearDBEntity entity in entities)
            {
                models.Add(GetOrgFiscalYearModel(entity));
            }

            return models;
        }

        private OrgFiscalYearModel GetOrgFiscalYearModel(OrgFiscalYearDBEntity entity)
        {
            return new OrgFiscalYearModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,                
                Organization = entity.Organization.Name,
                OrganizationId = entity.OrganizationId,
                RowTimeStamp = entity.RowTimeStamp,
                Id = entity.Id,
                FiscalYear = entity.FiscalYear.Name,
                FiscalYearId = entity.FiscalYearId,
                FiscalYearStart = entity.FiscalYearStart,
                FiscalYearEnd = entity.FiscalYearEnd
            };
        }

        private OrgFiscalYearDBEntity GetOrgFiscalYearDBEntity(OrgFiscalYearModel model)
        {
            return new OrgFiscalYearDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,                
                OrganizationId = model.OrganizationId,
                RowTimeStamp = model.RowTimeStamp,
                Id = model.Id,
                FiscalYearId = model.FiscalYearId,
                FiscalYearStart = model.FiscalYearStart,
                FiscalYearEnd = model.FiscalYearEnd
            };
        }
    }
}

