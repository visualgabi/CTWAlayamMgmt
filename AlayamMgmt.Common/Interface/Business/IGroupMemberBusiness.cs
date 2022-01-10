using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IGroupMemberBusiness : IBusiness<GroupMemberDBEntity> 
    {
        List<GroupMemberDBEntity> GetByGroupId(int groupId, bool? active);
        List<GroupMemberDBEntity> GetByGroupIds(List<int> groupIds, bool? active);
        List<GroupMemberDBEntity> GetByFamilyMemberId(int familyMemberId, bool? active);
        void SaveAll(List<GroupMemberDBEntity> groupMembers);


    }
}
