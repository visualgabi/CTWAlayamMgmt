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
     [RoutePrefix("api/MembershipStatus")]
    public class MembershipStatusController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("get")]
         public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<MembershipStatusDBEntity> membershipStatusBusiness = new BaseBusiness<MembershipStatusDBEntity>(Constant.DEFAULT_EMAIL);
                List<LookupModel> membershipStatusModels = GetLookModel<MembershipStatusDBEntity>(membershipStatusBusiness.Get(active));
                
                response = Request.CreateResponse(HttpStatusCode.OK, membershipStatusModels);
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
