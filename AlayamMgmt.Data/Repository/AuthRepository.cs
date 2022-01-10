using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Data.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Repository
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _authContext;

       //private UserManager<IdentityUser> _userManager;    
        private UserManager<IdentityUser> _userManager;    

        public AuthRepository()
        {
            _authContext = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_authContext));
        }

        //public async Task<IdentityResult> RegisterUser(UserDBEntity userDBEntity, string password)
        //{
        //    var result = await _userManager.CreateAsync(userDBEntity, password);
        //    return result;
        //}

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public ClientDBEntity FindClient(string clientId)
        {
            var client = _authContext.Clients.Where(x => x.Name == clientId).FirstOrDefault();
            
            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshTokenDBEntity token)
        {

            var existingToken = _authContext.RefreshTokens.Where(r => r.Email == token.Email && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _authContext.RefreshTokens.Add(token);

            return await _authContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _authContext.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _authContext.RefreshTokens.Remove(refreshToken);
                return await _authContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshTokenDBEntity refreshToken)
        {
            _authContext.RefreshTokens.Remove(refreshToken);
            return await _authContext.SaveChangesAsync() > 0;
        }

        public async Task<RefreshTokenDBEntity> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _authContext.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshTokenDBEntity> GetAllRefreshTokens()
        {
            return _authContext.RefreshTokens.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        //public Task<IdentityResult> Get()
        //{
        //    var client = _authContext.Clients.Find();

        //   // return client;
        //}


        public void Dispose()
        {
            _authContext.Dispose();
        }
    }


    public class UserModel
    {
       // [Required]
        //[Display(Name = "User name")]
        public string UserName { get; set; }

       // [Required]
       // [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
