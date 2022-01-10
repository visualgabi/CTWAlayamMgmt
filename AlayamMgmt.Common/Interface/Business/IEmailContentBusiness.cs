using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IEmailContentBusiness : IEditableBusiness<EmailContentDBEntity>, IReadOnlyBusiness<EmailContentDBEntity>
    {
        EmailContentDBEntity GetEmailContentByEmailType(int emailTypeId, int? OrgId, bool? active);
    }
}
