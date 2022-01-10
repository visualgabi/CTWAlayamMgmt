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
     [RoutePrefix("api/Language")]
    public class LanguageController : BaseApiController
    {
        [CustomAuthorizeAttribute]
        [Route("get")]
         public HttpResponseMessage Get(bool? active)
        {
            HttpResponseMessage response;

            try
            {
                IBusiness<LanguageDBEntity> languageBusiness = new BaseBusiness<LanguageDBEntity>(Constant.DEFAULT_EMAIL);
                List<LookupModel> languageBusinessModels = GetLookModel<LanguageDBEntity>(languageBusiness.Get(null));

                response = Request.CreateResponse(HttpStatusCode.OK, languageBusinessModels);
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
