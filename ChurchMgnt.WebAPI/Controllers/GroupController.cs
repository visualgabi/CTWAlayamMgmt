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
    [RoutePrefix("api/Group")]
    public class GroupController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("GetByOrgId")]
        public HttpResponseMessage GetByOrgId(int OrgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IGroupBusiness groupBusiness = new GroupBusiness(Constant.DEFAULT_EMAIL);
                List<OrganizationLookupModel> groupBusinessModels = GetOrganizationLookModels<GroupDBEntity>(groupBusiness.GetByOrgId(OrgId, active));

                response = Request.CreateResponse(HttpStatusCode.OK, groupBusinessModels);
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
        [Route("GetCustomAndBuildInGroupByOrgId")]
        public HttpResponseMessage GetCustomAndBuildInGroupByOrgId(int OrgId, bool? active)
        {
            HttpResponseMessage response;

            try
            {
                List<GroupBasicModel> models = new List<GroupBasicModel>();

                ClaimModel claimModel = GetClaimModel();


                models.Add(
                     new GroupBasicModel()
                     {
                         OrderId = 1,
                         Id = claimModel.OrgId.ToString() + "-" +  (int) BuildInEmailGroupType.Family,
                         Name = claimModel.OrganizationName + " - Family Group",
                         Type = EmailGroup.OrganizationGroup,
                         TypeInString = EmailGroup.OrganizationGroup.ToString()
                     }
                  );

                models.Add(
                       new GroupBasicModel()
                       {
                           OrderId = 2,
                           Id = claimModel.OrgId.ToString() + "-" + (int) BuildInEmailGroupType.Mens,
                           Name = claimModel.OrganizationName + " - Mens Group",
                           Type = EmailGroup.OrganizationGroup,
                           TypeInString = EmailGroup.OrganizationGroup.ToString()
                       }
                    );

                models.Add(
                       new GroupBasicModel()
                       {
                           OrderId = 3,
                           Id = claimModel.OrgId.ToString() + "-" + (int) BuildInEmailGroupType.Womens,
                           Name = claimModel.OrganizationName + " - Womens Group",
                           Type = EmailGroup.OrganizationGroup,                           
                           TypeInString = EmailGroup.OrganizationGroup.ToString()
                       }
                    );

                IOrganizationBusiness orgBusiness = new OrganizationBusiness(Constant.DEFAULT_EMAIL);
                List<OrganizationDBEntity> orgEntities = orgBusiness.GetBranchesByOrgId(claimModel.OrgId.Value, true);

                int orderId = getCustomGroupBaseModel(orgEntities, models, claimModel);

                IGroupBusiness groupBusiness = new GroupBusiness(Constant.DEFAULT_EMAIL);
                GetGroupBaseModel(groupBusiness.GetByOrgId(OrgId, active), orderId, models );

              
                

                response = Request.CreateResponse(HttpStatusCode.OK, models.OrderBy(x => x.OrderId));
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
                IGroupBusiness groupBusiness = new GroupBusiness(Constant.DEFAULT_EMAIL);
                byte[] returnValue = groupBusiness.Enable(enableModel.Id, enableModel.Active);

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
        public HttpResponseMessage Save(OrganizationLookupModel model)
        {
            HttpResponseMessage response;

            try
            {
                IGroupBusiness groupBusiness = new GroupBusiness(Constant.DEFAULT_EMAIL);
                groupBusiness.Save(GetGroupDBEntity(model));

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

        protected GroupDBEntity GetGroupDBEntity(OrganizationLookupModel model)
        {
            return new GroupDBEntity()
            {
                Active = model.Active,
                CreateDateTime = model.CreateDateTime,
                CreateUser = model.CreateUser,
                Description = model.Description,
                Id = model.Id,
                LastUpdateDateTime = model.LastUpdateDateTime,
                LastUpdateUser = model.LastUpdateUser,
                Name = model.Name,
                OrganizationId = model.OrganizationId,
                RowTimeStamp = model.RowTimeStamp
            };
        }

        protected void GetGroupBaseModel(List<GroupDBEntity> entities, int orderId, List<GroupBasicModel> models)
        {   
            foreach (GroupDBEntity entity in entities)
            {
                orderId++;

                models.Add(
                       new GroupBasicModel()
                      {
                          OrderId = orderId,
                          Id = entity.Id.ToString(),
                          Name = entity.Name,
                          Type = EmailGroup.CustomizedGroup,
                          TypeInString = EmailGroup.CustomizedGroup.ToString()

                       }
                    );
            }

          //  return models;
        }

        protected int getCustomGroupBaseModel(List<OrganizationDBEntity> entities, List<GroupBasicModel> models, ClaimModel claimModel)
        {            
            int iOrderPos = 3;

            foreach (OrganizationDBEntity entity in entities)
            {
                iOrderPos++;

                models.Add(
                       new GroupBasicModel()
                       {     
                           OrderId = iOrderPos,
                           Id = entity.ParentId.ToString() + "-" + entity.Id + "-" + (int) BuildInEmailGroupType.Family,
                           Name = claimModel.OrganizationName + " - " + entity.Name + " - Family Group",
                           Type = EmailGroup.BranchGroup,
                           TypeInString = EmailGroup.BranchGroup.ToString()
                       }
                    );

                models.Add(
                       new GroupBasicModel()
                       {
                           OrderId = iOrderPos,
                           Id = entity.ParentId.ToString() + "-" + entity.Id + "-"+ (int)BuildInEmailGroupType.Mens,
                           Name = claimModel.OrganizationName + " - " + entity.Name + " - Mens Group",
                           Type = EmailGroup.BranchGroup,
                           TypeInString = EmailGroup.BranchGroup.ToString()
                       }
                    );

                models.Add(
                       new GroupBasicModel()
                       {
                           OrderId = iOrderPos,
                           Id = entity.ParentId.ToString() + "-" + entity.Id + "-" + (int)BuildInEmailGroupType.Womens,
                           Name = claimModel.OrganizationName + " - " + entity.Name + " - Womens Group",
                           Type = EmailGroup.BranchGroup,
                           TypeInString = EmailGroup.BranchGroup.ToString()
                       }
                    );
            }

            return iOrderPos;
        }
    }
}
