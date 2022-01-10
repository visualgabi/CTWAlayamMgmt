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
     [RoutePrefix("api/TaxFiling")]
    public class TaxFilingController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        public HttpResponseMessage GetByOrgId(int OrgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                ITaxFilingBusiness taxFilingBusiness = new TaxFilingBusiness(Constant.DEFAULT_EMAIL);
                List<TaxFilingModel> taxFilingBusinessModels = GetTaxFilingModels(taxFilingBusiness.GetOrgId(OrgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, taxFilingBusinessModels);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetOrgId");
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
                ITaxFilingBusiness taxFilingBusiness = new TaxFilingBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = taxFilingBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(TaxFilingModel model)
        {
            HttpResponseMessage response;

            try
            {
                ITaxFilingBusiness taxFilingBusiness = new TaxFilingBusiness(Constant.DEFAULT_EMAIL);
                taxFilingBusiness.Save(GetTaxFilingDBEntity(model));

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
                        LogHelper.WriteToLog(Level.Information, string.Format(Constant.DEFAULT_UNIQUE_EXCEPTION_MSG, model.OrgFiscalYearId.ToString()), this.GetType(), "Save");
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

        private TaxFilingDBEntity GetTaxFilingDBEntity(TaxFilingModel model)
        {
            return new TaxFilingDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                Notes = model.Notes,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,                
                RowTimeStamp = model.RowTimeStamp,
                FiledBy = model.FiledBy,
                FiledDate = model.FiledDate,
                OrgFiscalYearId = model.OrgFiscalYearId,
                TotalAsset = model.TotalAsset,
                TotalExpense = model.TotalExpense,
                TotalLiability = model.TotalLiability,
                TotalRevenue = model.TotalRevenue
            };
        }

        private List<TaxFilingModel> GetTaxFilingModels(List<TaxFilingDBEntity> entities)
        {
            List<TaxFilingModel> models = new List<TaxFilingModel>();

            entities.ForEach(x => models.Add(GetTaxFilingModel(x)));

            return models;
        }

        private TaxFilingModel GetTaxFilingModel(TaxFilingDBEntity entity)
        {
            return new TaxFilingModel()
            {
                Active = entity.Active,
                CreateDateTime = entity.CreateDateTime,
                CreateUser = entity.CreateUser,
                Notes = entity.Notes,
                Id = entity.Id,
                LastUpdateDateTime = entity.LastUpdateDateTime,
                LastUpdateUser = entity.LastUpdateUser,
                RowTimeStamp = entity.RowTimeStamp,
                FiledBy = entity.FiledBy,
                FiledDate = entity.FiledDate,
                OrgFiscalYearId = entity.OrgFiscalYearId,
                TotalAsset = entity.TotalAsset,
                TotalExpense = entity.TotalExpense,
                TotalLiability = entity.TotalLiability,
                TotalRevenue = entity.TotalRevenue,
                OrgFiscalYear = entity.OrgFiscalYear.FiscalYear.Name
            };
        } 
    }
}
