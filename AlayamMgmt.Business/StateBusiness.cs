using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.BusinessLogic
{
    public class StateBusiness : BaseBusiness<StateDBEntity>, IStateBusiness
    {
        //private IUnitOfWork<StateDBEntity> _unitOfWork;

        public StateBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<StateDBEntity> GetStatesByCountryId(int countryId, bool? active)
        {
            if (active == null)
                return this._uow.GenericRepositoryObj.Get(x => x.CountryId == countryId).ToList();
            else
                return this._uow.GenericRepositoryObj.Get(x => x.CountryId == countryId && x.Active == active.Value).ToList();
        }

        
    }
}
