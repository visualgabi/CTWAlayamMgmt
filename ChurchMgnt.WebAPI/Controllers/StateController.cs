using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using ChurchMgnt.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
     [RoutePrefix("api/State")]
    public class StateController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("get")]
         public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IStateBusiness stateBusiness = new StateBusiness(Constant.DEFAULT_EMAIL);
                List<StateModel> stateBusinessModels = toSateModel(stateBusiness.Get(active));
                
                response = Request.CreateResponse(HttpStatusCode.OK, stateBusinessModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }
        }

        private List<StateModel> toSateModel(List<StateDBEntity> entities)
        {
            List<StateModel> models = new List<StateModel>();

            foreach (StateDBEntity entity in entities)
            {
                models.Add(new StateModel()
                {
                    Active = entity.Active,
                    CreateDateTime = entity.CreateDateTime,
                    CreateUser = entity.CreateUser,
                    Description = entity.Description,
                    Id = entity.Id,
                    LastUpdateDateTime = entity.LastUpdateDateTime,
                    LastUpdateUser = entity.LastUpdateUser,
                    Name = entity.Name,
                    CountryId = entity.CountryId
                }
                        );

            }

            return models;
        }
    }
}
