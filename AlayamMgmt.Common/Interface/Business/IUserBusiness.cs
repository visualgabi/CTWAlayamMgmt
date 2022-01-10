using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IUserBusiness : IEditableBusiness<UserDBEntity>, IReadOnlyBusiness<UserDBEntity>
    {
        UserDBEntity ValidateUser(string emailId, string password);
        List<UserDBEntity> GetByOrgId(int OrganizationId, bool? active);
        string ForgotPassword(string email);
        //UserDBEntity GetByEmail(string emailId);        
        //void ResetPassword(string emailId, string password, bool isTemp);
        //void LoginAtttempt(string emailId, bool success);
        //void ChangeUserStatus(int id, bool active);
        void EditUser(UserDBEntity entity);
        bool IsUnique(int id, string name);

        UserDBEntity GetPastorInfoByOrgId(int OrgId);

        void ChangePassword(int id, string newPassword);
    }
}
