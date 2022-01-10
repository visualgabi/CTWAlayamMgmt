using AlayamMgmt.Common.DBEntity;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    //[Table("Users")]
    //public class ApplicationUser : IdentityUser
    //{

    //    public ApplicationUser()
    //    {           
    //    }

    //    public async Task<ClaimsIdentity>
    //    GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        var userIdentity = await manager
    //            .CreateIdentityAsync(this,
    //                DefaultAuthenticationTypes.ApplicationCookie);
    //        return userIdentity;
    //    }

    //    [Required]
    //    public string FirstName { get; set; }
        
    //    [Required]
    //    public string LastName { get; set; }

    //    [ForeignKey("Organization"), Column("OrganizationId")]
    //    public int? OrganizationId { get; set; }

    //    public virtual OrganizationDBEntity Organization { get; set; }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext() : base("AuthContext") { }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {

    //        base.OnModelCreating(modelBuilder);
    //    }
    //}

    //public class ApplicationUserLogin : IdentityUserLogin
    //{
    //}

    //public class ApplicationUserClaim : IdentityUserClaim
    //{
    //}

    //public class ApplicationRole : IdentityRole
    //{
    //    public ApplicationRole()
    //        : base()
    //    {

    //    }

    //}

    //public class ApplicationUserRole : IdentityUserRole
    //{
    //    public ApplicationUserRole()
    //        : base()
    //    {

    //    }
    //}


}