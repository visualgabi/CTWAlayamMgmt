using AlayamMgmt.Business;
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Organization")]
    public class OrganizationController : BaseApiController
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

                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(claimModel.LoginUserEmail);
                List<OrganizationModel> organizationModels = GetOrganizationModels(organizationBusiness.Get(active));

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

        //[Authorize(Roles = "1")]
        [Route("GetById")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            HttpResponseMessage response;
            try
            {
                //ClaimModel claimModel = GetClaimModel();

                //IOrganizationBusiness organizationBusiness = new OrganizationBusiness(claimModel.LoginUserEmail);
                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
                OrganizationModel organizationModels = GetOrganizationModel(organizationBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, organizationModels);
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
                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
                OrganizationBasicModel organizationModels = GetOrganizationBasicModel(organizationBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, organizationModels);
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
        [Route("GetOrganization")]
        [HttpGet]
        public HttpResponseMessage GetOrganization()
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(claimModel.LoginUserEmail);
                OrganizationModel organizationModels = GetOrganizationModel(organizationBusiness.GetById(claimModel.OrgId.Value));

                response = Request.CreateResponse(HttpStatusCode.OK, organizationModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "GetOrganization");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }     

        

        [CustomAuthorizeAttribute]
        [Route("Save")]
        [HttpPost]
        public HttpResponseMessage Save([FromBody]OrganizationModel model)
        {
            HttpResponseMessage response;

            try
            {
                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
                organizationBusiness.Save(GetOrganizationDBEntity(model));

                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;                     
            }        
            catch (DbUpdateConcurrencyException ex)
            {
                LogHelper.WriteToLog(Level.Information, Constant.DEFAULT_CONFLICT_EXCEPTION_MSG, this.GetType(), "Save");
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.Concurrency );
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
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int) ExceptionType.Unique);
                        return response;
                    }
                }

                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Save");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;   
            }    
            catch(Exception ex)
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
                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
                bool returnValue = organizationBusiness.IsUnique(id, name);

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
                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = organizationBusiness.Enable(enableModel.Id, enableModel.Active);

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

        //[CustomAuthorizeAttribute]
        //[Route("GetBranches")]
        //[HttpGet]
        //public HttpResponseMessage GetBranches()
        //{
        //    HttpResponseMessage response;
        //    try
        //    {
        //        ClaimModel claimModel = GetClaimModel();

        //        IOrganizationBusiness organizationBusiness = new OrganizationBusiness(claimModel.LoginUserEmail);
        //        List<OrganizationModel> organizationModels = GetOrganizationModels(organizationBusiness.GetBranches(claimModel.OrgId.Value,null));

        //        response = Request.CreateResponse(HttpStatusCode.OK, organizationModels);
        //        return response;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
        //        return response;
        //    }
        //}

        [CustomAuthorizeAttribute]
        [Route("GetBranchesByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetBranchesByOrgId(int orgId, bool? active)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(claimModel.LoginUserEmail);
                List<OrganizationModel> organizationModels = GetOrganizationModels(organizationBusiness.GetBranchesByOrgId(orgId, active));

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
        [Route("GetBranch")]
        [HttpGet]
        public HttpResponseMessage GetBranch(int id)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrganizationBusiness organizationBusiness = new OrganizationBusiness(claimModel.LoginUserEmail);
                OrganizationModel organizationModel = GetOrganizationModel(organizationBusiness.GetById(id));

                response = Request.CreateResponse(HttpStatusCode.OK, organizationModel);
                return response;

            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        private List<OrganizationModel> GetOrganizationModels(List<OrganizationDBEntity> organizationDBEntities)
        {
            List<OrganizationModel> organizationModels = new List<OrganizationModel>();

            foreach(OrganizationDBEntity x in organizationDBEntities)
            {
                 organizationModels.Add( GetOrganizationModel(x) );
            };

            return organizationModels;

        }

        private OrganizationModel GetOrganizationModel(OrganizationDBEntity organizationDBEntity)
        {
                return new OrganizationModel()
                {
                    Active = organizationDBEntity.Active,
                    Address1 = organizationDBEntity.Address1,
                    Address2 = organizationDBEntity.Address2,
                    Address3 = organizationDBEntity.Address3,
                    City = organizationDBEntity.City,
                    Country = organizationDBEntity.Country.Name,
                    CountryId = organizationDBEntity.CountryId,
                    CreateDateTime = organizationDBEntity.CreateDateTime,
                    CreateUser = organizationDBEntity.CreateUser,
                    DenominationId = organizationDBEntity.DenominationId,
                    Denomination = organizationDBEntity.Denomination != null ? organizationDBEntity.Denomination.Name : null,                    
                    Discription = organizationDBEntity.Discription,
                    EmailId1 = organizationDBEntity.EmailId1,
                    EmailId2 = organizationDBEntity.EmailId2,
                    EthnicOrigin = organizationDBEntity.EthnicOrigin.Name,
                    EthnicOriginId = organizationDBEntity.EthnicOriginId,
                    Id = organizationDBEntity.Id,
                    LastUpdateDateTime = organizationDBEntity.LastUpdateDateTime,
                    LastUpdateUser = organizationDBEntity.LastUpdateUser,
                    Name = organizationDBEntity.Name,
                    Phone1 = organizationDBEntity.Phone1,
                    Phone2 = organizationDBEntity.Phone2,
                    PrimaryLanguage = organizationDBEntity.PrimaryLanguage.Name,
                    PrimaryLanguageId = organizationDBEntity.PrimaryLanguageId,
                    SecondaryLanguage = organizationDBEntity.SecondaryLanguage.Name,
                    SecondaryLanguageId = organizationDBEntity.SecondaryLanguageId,
                    State = organizationDBEntity.State.Name,
                    StateId = organizationDBEntity.StateId,
                    Website = organizationDBEntity.Website,
                    ZipCode = organizationDBEntity.ZipCode,
                    RowTimeStamp = organizationDBEntity.RowTimeStamp,
                    ParentId = organizationDBEntity.ParentId,
                    TaxId = organizationDBEntity.TaxId,
                    CurrencyId = organizationDBEntity.CurrencyId,
                    Currency = organizationDBEntity.Currency != null ? organizationDBEntity.Currency.Name : "$",                    
                    FiscalYearStartAndEndMonthId = organizationDBEntity.FiscalYearStartAndEndMonthId,
                    FiscalYearStartAndEndMonth = organizationDBEntity.FiscalYearStartAndEndMonthId != null? organizationDBEntity.FiscalYearStartAndEndMonth.Name : null                  
                };
        }

        private OrganizationBasicModel GetOrganizationBasicModel(OrganizationDBEntity organizationDBEntity)
        {
            return new OrganizationBasicModel()
            {                
                Address1 = organizationDBEntity.Address1,             
                City = organizationDBEntity.City,
                Country = organizationDBEntity.Country.Name,                
                EmailId1 = organizationDBEntity.EmailId1,                                
                Name = organizationDBEntity.Name,
                Phone1 = organizationDBEntity.Phone1,                
                State = organizationDBEntity.State.Name,                
                Website = organizationDBEntity.Website,
                ZipCode = organizationDBEntity.ZipCode,
                TaxId = organizationDBEntity.TaxId,                
                Currency = organizationDBEntity.Currency != null ? organizationDBEntity.Currency.Name : "$",                
            };
        }

        private OrganizationDBEntity GetOrganizationDBEntity(OrganizationModel model)
        {
            return new OrganizationDBEntity()
            {
                Active = model.Active,
                Address1 = model.Address1,
                Address2 = model.Address2,
                Address3 = model.Address3,
                City = model.City,                
                CountryId = model.CountryId,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,                
                DenominationId = model.DenominationId,
                Discription = model.Discription,
                EmailId1 = model.EmailId1,
                EmailId2 = model.EmailId2,                
                EthnicOriginId = model.EthnicOriginId,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                Name = model.Name,
                Phone1 = model.Phone1,
                Phone2 = model.Phone2,                
                PrimaryLanguageId = model.PrimaryLanguageId,                
                SecondaryLanguageId = model.SecondaryLanguageId,                
                StateId = model.StateId,
                Website = model.Website,
                ZipCode = model.ZipCode,
                RowTimeStamp = model.RowTimeStamp,
                ParentId = model.ParentId,
                TaxId = model.TaxId,
                CurrencyId = model.CurrencyId,
                FiscalYearStartAndEndMonthId = model.FiscalYearStartAndEndMonthId                
            };

        }        
    }
     
    public class EnableModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }
}
