using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IMessageBusiness : IBusiness<MessageDBEntity>   
    {
        MessageDBEntity getByOrgIdAndMsgTypeId(int orgId, int msgTypeId);
    }
}
