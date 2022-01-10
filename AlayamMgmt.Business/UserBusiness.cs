using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.Interface.Data;
//using AlayamMgmt.Common.Interface.Security;
//using AlayamMgmt.Common.Security;
//using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common;
//using DYIL.ICMSS.Common.DBEntity;

namespace AlayamMgmt.BusinessLogic
{
    public class UserBusiness : BaseBusiness<UserDBEntity>, IUserBusiness
    {

        public UserBusiness(string aduitUser)
            : base(aduitUser)
        { }

        public UserDBEntity GetPastorInfoByOrgId(int OrgId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.UserRoles.Any(y => y.OrganizationId == OrgId && y.RoleId == (int) Role.LeadPastor) && x.Active == true ).FirstOrDefault();            
        }

        public UserDBEntity ValidateUser(string emailId, string password)
        {
            string encryptionKey = ConfigurationManager.AppSettings["encryptionKey"].ToString();

            IHasher hasher = new HMACSHA512Hasher(encryptionKey);
            string hashPassword = hasher.Hash(password);

            return this._uow.GenericRepositoryObj.Get(x => x.UserName == emailId && x.Active == true && x.PasswordHash == hashPassword).FirstOrDefault();
        }

        public string ForgotPassword(string email)
        {
            string encryptionKey = ConfigurationManager.AppSettings["encryptionKey"].ToString();

            IHasher hasher = new HMACSHA512Hasher(encryptionKey);
            string newPassword = hasher.GetUniqueKey(8);
            string hashPassword = hasher.Hash(newPassword);

            UserDBEntity entity = this._uow.GenericRepositoryObj.Get(x => x.UserName == email).FirstOrDefault();
            entity.PasswordHash = hashPassword;

            this._uow.GenericRepositoryObj.Save(entity);

            return encryptionKey;
        }

        public List<UserDBEntity> GetByOrgId(int OrganizationId, bool? active)
        {
            List<UserDBEntity> entities = new List<UserDBEntity>();

            if (active == null)
            {
                entities = this._uow.GenericRepositoryObj.Get(x => x.UserRoles.Any(y => y.OrganizationId == OrganizationId)).ToList();

            }
            else
            {
                entities = this._uow.GenericRepositoryObj.Get(x => x.UserRoles.Any(y => y.OrganizationId == OrganizationId) && x.Active == active.Value).ToList();
            }

            return entities;
        }

        public bool IsUnique(int id, string name)
        {
            //while adding 
            if (id == 0)
                return this._uow.GenericRepositoryObj.Get(x => x.UserName.Trim().ToLower() == name.Trim().ToLower()).Count() == 0;
            else
                //while editing 
                return this._uow.GenericRepositoryObj.Get(x => x.UserName.Trim().ToLower() == name.Trim().ToLower() && x.Id != id).Count() == 0;
        }

        public void ChangePassword(int id, string newPassword)
        {
            string encryptionKey = ConfigurationManager.AppSettings["encryptionKey"].ToString();

            IHasher hasher = new HMACSHA512Hasher(encryptionKey);
            string hashPassword = hasher.Hash(newPassword);


            UserDBEntity userDBEntity = new UserDBEntity();
            userDBEntity = this._uow.GenericRepositoryObj.Get(x => x.Id == id).FirstOrDefault();

            userDBEntity.PasswordHash = hashPassword;

            if (userDBEntity != null)
            {
                this.Save(userDBEntity);
                this._uow.Commit();
            }
            else
                throw new Exception("Invalid User");
        }

        public void EditUser(UserDBEntity entity)
        {
            UserDBEntity userDBEntity = new UserDBEntity();
            userDBEntity = this._uow.GenericRepositoryObj.Get(x => x.Id == entity.Id).FirstOrDefault();

            userDBEntity.UserName = entity.UserName;
            userDBEntity.FirstName = entity.FirstName;
            userDBEntity.LastName = entity.LastName;
            userDBEntity.UserRoles.FirstOrDefault().RoleId = entity.UserRoles.FirstOrDefault().RoleId;
            userDBEntity.UserRoles.FirstOrDefault().OrganizationId = entity.UserRoles.FirstOrDefault().OrganizationId;

            if (userDBEntity != null)
            {                
                this.Save(userDBEntity);
                this._uow.Commit();
            }
            else
                throw new Exception("Invalid User");
        }

       
        //public void ResetPassword(string emailId, string password, bool isTemp)
        //{
        //     string encryptionKey = ConfigurationManager.AppSettings["encryptionKey"].ToString();
        //    IHasher hasher = new HMACSHA512Hasher(encryptionKey);            
        //    password = hasher.Hash(password);    

        //    UserDBEntity userDBEntity = new UserDBEntity();
        //    userDBEntity = this._uow.GenericRepositoryObj.Get(x => x.Email == emailId).FirstOrDefault();

        //    if (userDBEntity != null)
        //    {
        //        userDBEntity.TemporaryPassword = isTemp;
        //        userDBEntity.PasswordHash = password;

        //        this.Save(userDBEntity);
        //        this._uow.Commit();
        //    }
        //    else
        //        throw new Exception("Invalid User");
        //}


        //public UserDBEntity GetByEmail(string emailId)
        //{
        //    return this._uow.GenericRepositoryObj.Get(x => x.Email == emailId && x.Active == true).FirstOrDefault();           
        //}

        //public void LoginAtttempt(string emailId, bool success)
        //{
        //    UserDBEntity userDBEntity = new UserDBEntity();
        //    userDBEntity = this._uow.GenericRepositoryObj.Get(x => x.Email == emailId && x.Active == true).FirstOrDefault();
        //    if( userDBEntity != null)
        //    {
        //        if( success == true)
        //        {
        //            userDBEntity.AccessFailedCount = 0;
        //            userDBEntity.LockoutDateTime = null;
        //        }
        //        else
        //        {
        //            userDBEntity.AccessFailedCount = userDBEntity.AccessFailedCount + 1;
        //            userDBEntity.LockoutDateTime = DateTime.Now;
        //        }

        //        this.Save(userDBEntity);
        //        this._uow.Commit();
        //    }
        //}

        //public void ChangeUserStatus(int id, bool active)
        //{
        //    UserDBEntity userDBEntity = new UserDBEntity();
        //    userDBEntity = this._uow.GenericRepositoryObj.Get(x => x.Id == id && x.Active == true).FirstOrDefault();
        //    if (userDBEntity != null)
        //    {
        //        userDBEntity.Active = active; 
        //        this.Save(userDBEntity);
        //        this._uow.Commit();
        //    }
        //}






        
    }
}
