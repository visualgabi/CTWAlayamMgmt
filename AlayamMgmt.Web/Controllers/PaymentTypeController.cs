using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Helper;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Web.Models;
using AlayamMgmt.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlayamMgmt.Web.Controllers
{
    [RoutePrefix("api/PaymentType")]
    public class PaymentTypeController : BaseApiController
    {
        //[CustomAuthorizeAttribute]
         [CustomAuthorizeAttribute]
        [Route("get")]
        public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<PaymentTypeDBEntity> paymentTypeBusiness = new BaseBusiness<PaymentTypeDBEntity>(Constant.DEFAULT_EMAIL);
                List<LookupModel> paymentTypeModels = GetLookModel<PaymentTypeDBEntity>(paymentTypeBusiness.Get(active));
                
                response = Request.CreateResponse(HttpStatusCode.OK, paymentTypeModels);
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
