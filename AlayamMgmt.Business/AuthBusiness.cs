using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Data.Repository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AlayamMgmt.Business
{
    public class AuthBusiness
    {
        public ClientDBEntity FindClient(string clientId)
        {
            ClientDBEntity clientEntity = new ClientDBEntity();

            using (AuthRepository _repo = new AuthRepository())
            {
                clientEntity = _repo.FindClient(clientId);
            }
            return clientEntity;
        }

        
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = null;

            using (AuthRepository _repo = new AuthRepository())
            {
                user = await _repo.FindUser(userName, password);
            }
            return user;
        }

        public async Task<bool> AddRefreshToken(RefreshTokenDBEntity token)
        {
            bool returnValue = false;

            using (AuthRepository _repo = new AuthRepository())
            {
                returnValue = await _repo.AddRefreshToken(token);
            }
            return returnValue;
        }

        public async Task<RefreshTokenDBEntity> FindRefreshToken(string refreshTokenId)
        {
            RefreshTokenDBEntity refreshTokenEntity;

            using (AuthRepository _repo = new AuthRepository())
            {
                refreshTokenEntity = await _repo.FindRefreshToken(refreshTokenId);
            }            

            return refreshTokenEntity;
        }

        public async Task<bool> RemoveRefreshToken(RefreshTokenDBEntity refreshToken)
        {
            bool returnValue = false;

            using (AuthRepository _repo = new AuthRepository())
            {
                returnValue = await _repo.RemoveRefreshToken(refreshToken);
            }

            return returnValue;
        }

        //public async Task<bool> Get()
        //{
        //    bool returnValue = false;

        //    using (AuthRepository _repo = new AuthRepository())
        //    {
        //        returnValue = await _repo.RemoveRefreshToken(refreshToken);
        //    }

        //    return returnValue;
        //}
    }
}
