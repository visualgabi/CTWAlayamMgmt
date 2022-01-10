using AlayamMgmt.Web.Providers;
using System.Web;
using System.Web.Mvc;

namespace AlayamMgmt.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());            
        }
    }
}
