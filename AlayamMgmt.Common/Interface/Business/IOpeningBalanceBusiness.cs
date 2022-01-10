using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IOpeningBalanceBusiness : IBusiness<OpeningBalanceDBEntity>  
    {
        List<OpeningBalanceDBEntity> GetByOrgId(int orgId, bool? active);
    }
}
