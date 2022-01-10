using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChurchMgnt.WebAPI.Controllers
{
    [RoutePrefix("api/Info")]
    public class InfoController : ApiController
    {
        [HttpGet]
        public string Version()
        {
            return ConfigurationManager.AppSettings["appVersion"].ToString();
        }
    }
}
