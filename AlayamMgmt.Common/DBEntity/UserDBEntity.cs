namespace AlayamMgmt.Common.DBEntity
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

    [Table("Users")]
    public partial class UserDBEntity : BaseDBEntity
    {
        public UserDBEntity() : base()
        { 
            UserRoles = new HashSet<UserRoleDBEntity>();          
        }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }        

        [Required]
        [StringLength(250)]
        public string PasswordHash { get; set; }

        public virtual ICollection<UserRoleDBEntity> UserRoles { get; set; }        
    }


    [Table("Roles")]
    public partial class RoleDBEntity : BaseDBEntity
    {
        public RoleDBEntity()
            : base()
        {
            UserRoles = new HashSet<UserRoleDBEntity>();
        }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }      

        public virtual ICollection<UserRoleDBEntity> UserRoles { get; set; }

    }

    [Table("UserRoles")]
    public partial class UserRoleDBEntity : BaseDBEntity
    {
        public UserRoleDBEntity()
            : base()
        {            
        }      

        [Required]
        [ForeignKey("User"), Column("UserId")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Role"), Column("RoleId")]
        public int RoleId { get; set; }
                
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int? OrganizationId { get; set; }

        public virtual UserDBEntity User { get; set; }

        public virtual RoleDBEntity Role { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }        

    }
}
