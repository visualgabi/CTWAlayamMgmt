using AlayamMgmt.BusinessLogic;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class FamilyPledgeBusiness : BaseBusiness<FamilyPledgeDBEntity>, IFamilyPledgeBusiness
    {
        public FamilyPledgeBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<FamilyPledgeDBEntity> GetPledgesByFamilyId(int familyId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.Active == active).ToList();
        }

        public List<FamilyPledgeDBEntity> GetPledgesByFamilyIdAndOrgFiscalYearId(int familyId, int orgFiscalYearId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.OrgFiscalYearId == orgFiscalYearId && x.Active == true).ToList();
        }

        public List<FamilyPledgeDBEntity> GetPledgesByFamilyIdAndOfferingTypeId(int familyId, int fundTypeId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.FundTypeId == fundTypeId).ToList();
        }

        public FamilyPledgeDBEntity GetPledgesByFIamilydAndOrgFiscalYearIdAndOfferingTypeId(int familyId, int orgFiscalYearId, int fundTypeId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.FamilyId == familyId && x.OrgFiscalYearId == orgFiscalYearId && x.FundTypeId == fundTypeId).FirstOrDefault();
        }

        public List<FamilyPledgeDBEntity> GetPledgesByOrgId(int orgId, bool? active)
        {
            if(active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.OrgFiscalYear.OrganizationId == orgId && x.Active == active).ToList();
        }
      
    }
}
