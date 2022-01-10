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
    public class OrgProfileController : BaseApiController
    {
        [Authorize(Roles = "1")]
        [Route("GetProfileByOrgId")]
        [HttpGet]
        public HttpResponseMessage GetProfileByOrgId(int orgId)
        {
            HttpResponseMessage response;
            try
            {
                ClaimModel claimModel = GetClaimModel();

                IOrgProfileBusiness orgProfileBusiness = new OrgProfileBusiness(claimModel.LoginUserEmail);
                OrgProfileModel orgProfileModels = GetOrgProfileModel(orgProfileBusiness.GetProfile(orgId));

                response = Request.CreateResponse(HttpStatusCode.OK, orgProfileModels);
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
        public HttpResponseMessage Save([FromBody]OrgProfileModel model)
        {
            HttpResponseMessage response;

            try
            {
                IOrgProfileBusiness orgProfileBusiness = new OrgProfileBusiness(Constant.DEFAULT_EMAIL);
                orgProfileBusiness.Save(GetOrgProfileDBEntity(model));

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
                IOrgProfileBusiness orgProfileBusiness = new OrgProfileBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = orgProfileBusiness.Enable(enableModel.Id, enableModel.Active);

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


        private List<OrgProfileModel> GetOrgProfileModels(List<OrgProfileDBEntity> entities)
        {
            List<OrgProfileModel> models = new List<OrgProfileModel>();

            foreach(OrgProfileDBEntity entity in entities)
            {
                models.Add(GetOrgProfileModel(entity));
            }

            return models;
        }

        private OrgProfileModel GetOrgProfileModel(OrgProfileDBEntity entity)
        {
            return new OrgProfileModel()
                {
                    Active = entity.Active,
                    CreateDateTime = entity.CreateDateTime,
                    CreateUser = entity.CreateUser,
                    LastUpdateDateTime = entity.LastUpdateDateTime,
                    LastUpdateUser = entity.LastUpdateUser,
                    CurrencyId = entity.CurrencyId,
                    Currency = entity.Currency.Name,
                    Organization = entity.Organization.Name,
                    OrganizationId = entity.OrganizationId,
                    RowTimeStamp = entity.RowTimeStamp,
                    Id = entity.Id
                };
        }

        private OrgProfileDBEntity GetOrgProfileDBEntity(OrgProfileModel model)
        {
            return new OrgProfileDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                CurrencyId = model.CurrencyId,
                OrganizationId = model.OrganizationId,
                RowTimeStamp = model.RowTimeStamp
            };
        }
    }
}
