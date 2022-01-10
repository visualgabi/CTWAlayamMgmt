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
    public class GroupMemberBusiness : BaseBusiness<GroupMemberDBEntity>, IGroupMemberBusiness
    {
        public GroupMemberBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public List<GroupMemberDBEntity> GetByFamilyMemberId(int familyMemberId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.FamilyMemberId == familyMemberId && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.FamilyMemberId == familyMemberId).ToList();
        }

        public List<GroupMemberDBEntity> GetByGroupId(int groupId, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.GroupId == groupId && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => x.GroupId == groupId).ToList();
        }

        public List<GroupMemberDBEntity> GetByGroupIds(List<int> groupIds, bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => groupIds.Contains(x.GroupId) && x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get(x => groupIds.Contains(x.GroupId)).ToList();
        }

        public void SaveAll(List<GroupMemberDBEntity> groupMembers)
        {
           foreach(GroupMemberDBEntity entity in groupMembers)
            {
                _uow.GenericRepositoryObj.Save(entity);
            }

            _uow.Commit();
        }
    }
}
