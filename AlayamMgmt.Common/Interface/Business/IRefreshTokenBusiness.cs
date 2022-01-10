using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IRefreshTokenBusiness : IBusiness<RefreshTokenDBEntity>   
    {
        RefreshTokenDBEntity GetByTokenId(string tokenId);
        void AddOrUpdate(RefreshTokenDBEntity refreshToken);
        void RemoveRefreshToken(string tokenId);
    }
}
