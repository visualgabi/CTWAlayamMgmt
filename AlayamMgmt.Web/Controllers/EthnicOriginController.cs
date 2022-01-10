using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/EthnicOrigin")]
    public class EthnicOriginController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("get")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<EthnicOriginDBEntity> ethnicOriginBusiness = new BaseBusiness<EthnicOriginDBEntity>(Constant.DEFAULT_EMAIL);
                List<LookupModel> ethnicOriginModels = GetLookModel<EthnicOriginDBEntity>(ethnicOriginBusiness.Get(active));

                response = Request.CreateResponse(HttpStatusCode.OK, ethnicOriginModels);
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
