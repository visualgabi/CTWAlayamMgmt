using AlayamMgmt.Business;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
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
     [RoutePrefix("api/OrgFiscalYearBudget")]
    public class OrgFiscalYearBudgetController : BaseApiController
    {
        [Authorize()]
        [Route("Get")]
        [HttpGet]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgFiscalYearBudgetBusiness orgFiscalYearBudgetBusiness = new OrgFiscalYearBudgetBusiness(claimModel.LoginUserEmail);
                List<OrgFiscalYearBudgetModel> organizationModels = GetOrgFiscalYearBudgetModels(orgFiscalYearBudgetBusiness.Get(active));

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


        [Authorize()]
        [Route("GetOrgFiscalYearBudgetsByOrgIdAndFiscalYearId")]
        [HttpGet]
        public HttpResponseMessage GetOrgFiscalYearBudgetsByOrgIdAndFiscalYearId(int orgId, int fiscalYearId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgFiscalYearBudgetBusiness orgFiscalYearBudgetBusiness = new OrgFiscalYearBudgetBusiness(claimModel.LoginUserEmail);
                List<OrgFiscalYearBudgetModel> organizationModels = GetOrgFiscalYearBudgetModels(orgFiscalYearBudgetBusiness.GetOrgFiscalYearBudgetsByOrgIdAndFiscalYearId(orgId, fiscalYearId, active));

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

        [Authorize()]
        [Route("GetOrgFiscalYearBudgetsByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetOrgFiscalYearBudgetsByOrgIdAndFiscalYearId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgFiscalYearBudgetBusiness orgFiscalYearBudgetBusiness = new OrgFiscalYearBudgetBusiness(claimModel.LoginUserEmail);
                List<OrgFiscalYearBudgetModel> organizationModels = GetOrgFiscalYearBudgetModels(orgFiscalYearBudgetBusiness.GetOrgFiscalYearBudgetsByOrgId(orgId, active));

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

        [Authorize()]
        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgFiscalYearBudgetBusiness orgFiscalYearBudgetBusiness = new OrgFiscalYearBudgetBusiness(claimModel.LoginUserEmail);
                OrgFiscalYearBudgetModel orgFiscalYearBudgetModels = GetOrgFiscalYearBudgetModel(orgFiscalYearBudgetBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, orgFiscalYearBudgetModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetById");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]OrgFiscalYearBudgetModel model)
        {
            HttpResponseMessage response;

            try
            {
                IOrgFiscalYearBudgetBusiness orgFiscalYearBudgetBusiness = new OrgFiscalYearBudgetBusiness(Constant.DEFAULT_EMAIL);
                orgFiscalYearBudgetBusiness.Save(GetOrgFiscalYearBudgetDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.FundType), this.GetType(), "Save");
                        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Unique);
                        return response;
                    }
                }

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
                IOrgFiscalYearBudgetBusiness orgFiscalYearBudgetBusiness = new OrgFiscalYearBudgetBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = orgFiscalYearBudgetBusiness.Enable(enableModel.Id, enableModel.Active);

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

        private List<OrgFiscalYearBudgetModel> GetOrgFiscalYearBudgetModels(List<OrgFiscalYearBudgetDBEntity> entities)
        {
            List<OrgFiscalYearBudgetModel> models = new List<OrgFiscalYearBudgetModel>();

            foreach (OrgFiscalYearBudgetDBEntity entity in entities)
            {
                models.Add(GetOrgFiscalYearBudgetModel(entity));
            }

            return models;
        }

        private OrgFiscalYearBudgetModel GetOrgFiscalYearBudgetModel(OrgFiscalYearBudgetDBEntity entity)
        {
            return new OrgFiscalYearBudgetModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,               
                RowTimeStamp = entity.RowTimeStamp,
                Id = entity.Id,
                OrgFiscalYear = entity.OrgFiscalYear.FiscalYear.Name.ToString(),
                OrgFiscalYearId = entity.OrgFiscalYearId,
                FundType = entity.FundType.Name,
                FundTypeId = entity.FundTypeId,
                Amount = entity.Amount
            };
        }

        private OrgFiscalYearBudgetDBEntity GetOrgFiscalYearBudgetDBEntity(OrgFiscalYearBudgetModel model)
        {
            return new OrgFiscalYearBudgetDBEntity()
            {
                Id = model.Id,
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,               
                RowTimeStamp = model.RowTimeStamp,                
                OrgFiscalYearId = model.OrgFiscalYearId,                
                FundTypeId = model.FundTypeId,
                Amount = model.Amount
            };
        }
    }
}
