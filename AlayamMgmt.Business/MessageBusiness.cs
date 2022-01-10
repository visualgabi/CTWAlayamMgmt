using AlayamMgmt.Common.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.BusinessLogic;

namespace AlayamMgmt.Business
{
    public class MessageBusiness : BaseBusiness<MessageDBEntity>, IMessageBusiness
    {
        public MessageBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public MessageDBEntity getByOrgIdAndMsgTypeId(int orgId, int msgTypeId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.OrganizationId == orgId && x.MessageTypeId == msgTypeId && x.Active == true).FirstOrDefault();
        }
    }
}
