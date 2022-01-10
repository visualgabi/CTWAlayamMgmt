using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IReportBusiness<TResult> where TResult : class
    {
        TResult Fetch(BaseCriteria criteria);
        List<TResult> FetchAll(BaseCriteria criteria);
    }
}
