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
    public class RefreshTokenBusiness : BaseBusiness<RefreshTokenDBEntity>, IRefreshTokenBusiness
    {
        public RefreshTokenBusiness(string aduitUser)
            : base(aduitUser)
        { }


        public RefreshTokenDBEntity GetByTokenId(string tokenId)
        {
            return this._uow.GenericRepositoryObj.Get(x => x.TokenId == tokenId && x.Active == true).FirstOrDefault();
        }



        public void RemoveRefreshToken(string tokenId)
        {
            RefreshTokenDBEntity entity = this._uow.GenericRepositoryObj.Get(x => x.TokenId == tokenId && x.Active == true).FirstOrDefault();
            if (entity != null)
            {
                entity.Active = false;
                this._uow.GenericRepositoryObj.Save(entity);
                this._uow.Commit();
            }
        }


        public void AddOrUpdate(RefreshTokenDBEntity refreshToken)
        {
            RefreshTokenDBEntity entity = this._uow.GenericRepositoryObj.Get(x => x.Email == refreshToken.Email && x.TokenId == refreshToken.TokenId && x.Active == true).FirstOrDefault();
            if (entity != null)
            {
                entity.Active = false;
                this._uow.GenericRepositoryObj.Save(entity);
            }

            this._uow.GenericRepositoryObj.Save(refreshToken);
            this._uow.Commit();

        }
    }
}
