using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface ISponsorBusiness : IBusiness<SponsorDBEntity>   
    {
        List<SponsorDBEntity> GetByOrgId(int orgId, bool? active);
        bool IsUnique(int id, string name);       
    }
}
