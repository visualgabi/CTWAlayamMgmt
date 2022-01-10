using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IStateBusiness : IBusiness<StateDBEntity>
    {
        List<StateDBEntity> GetStatesByCountryId(int countryId, bool? active);
    }
}
