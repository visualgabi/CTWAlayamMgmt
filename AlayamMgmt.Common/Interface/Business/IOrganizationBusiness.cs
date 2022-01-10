using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IOrganizationBusiness : IBusiness<OrganizationDBEntity>   
    {
       bool IsUnique(int id, string name);       
       List<OrganizationDBEntity> GetBranchesByOrgId(int orgId, bool? active);
    }
}
