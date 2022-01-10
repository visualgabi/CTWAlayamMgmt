using AlayamMgmt.Common.DBEntity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Context
{
    public class AuthContext : IdentityDbContext<IdentityUser>    
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public DbSet<ClientDBEntity> Clients { get; set; }
        public DbSet<RefreshTokenDBEntity> RefreshTokens { get; set; }
    }
}

