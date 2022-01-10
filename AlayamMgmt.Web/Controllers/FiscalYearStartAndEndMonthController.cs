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
    [RoutePrefix("api/FiscalYearStartAndEndMonth")]
    public class FiscalYearStartAndEndMonthController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("get")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<FiscalYearStartAndEndMonthDBEntity> yearStartAndEndMonthBusiness = new BaseBusiness<FiscalYearStartAndEndMonthDBEntity>(Constant.DEFAULT_EMAIL);
                List<LookupModel> models = GetLookModel<FiscalYearStartAndEndMonthDBEntity>(yearStartAndEndMonthBusiness.Get(null));

                response = Request.CreateResponse(HttpStatusCode.OK, models);
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
