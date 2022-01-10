using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IFamilyPledgeBusiness : IBusiness<FamilyPledgeDBEntity>   
    {
        List<FamilyPledgeDBEntity> GetPledgesByOrgId(int orgId, bool? active);

        List<FamilyPledgeDBEntity> GetPledgesByFamilyId(int familyId, bool? active);       

        List<FamilyPledgeDBEntity> GetPledgesByFamilyIdAndOrgFiscalYearId(int familyId, int orgFiscalYearId);

        List<FamilyPledgeDBEntity> GetPledgesByFamilyIdAndOfferingTypeId(int familyId, int fundTypeId);

        FamilyPledgeDBEntity GetPledgesByFIamilydAndOrgFiscalYearIdAndOfferingTypeId(int familyId, int orgFiscalYearId, int fundTypeId);
    }
}
