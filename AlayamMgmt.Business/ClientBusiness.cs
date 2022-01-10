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
    public class ClientBusiness : BaseBusiness<ClientDBEntity>, IClientBusiness
    {
        public ClientBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public ClientDBEntity getClientByName(string clientName)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.Name == clientName).First();
        }
    }
}
