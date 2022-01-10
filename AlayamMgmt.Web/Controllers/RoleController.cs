
using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Mapper;
using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        [CustomAuthorizeAttribute]
        [Route("Get")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<RoleDBEntity> roleBusiness = new BaseBusiness<RoleDBEntity>(Constant.DEFAULT_EMAIL);
                List<RoleModel> roleModels = DBEntityToModel.GetRoleModels(roleBusiness.Get(active));


                response = Request.CreateResponse(HttpStatusCode.OK, roleModels);
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog(Level.Error, ex, this.GetType(), "Get");
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, (int)ExceptionType.InternalServer);
                return response;
            }

        }
    }
}
