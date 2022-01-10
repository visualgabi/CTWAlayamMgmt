using AlayamMgmt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AlayamMgmt.Web.Interface
{
    public interface ILogoManager
    {
        Task<IEnumerable<LogoModel>> Get();
        Task<LogoActionResult> Delete(string fileName);
        Task<IEnumerable<LogoModel>> Add(HttpRequestMessage request);
        bool FileExists(string fileName);
    }
}