using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{    
    public partial class UserModel : BaseModel
    {
        public UserModel()
            : base()
        {
            
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
        public string Password { get; set; }
        
        public int RoleId { get; set; }

        public int? OrganizationId { get; set; }               

        public string Role { get; set; }

        public string Organization { get; set; }

        public string Currency { get; set; }        
    }

    public partial class RoleModel : BaseModel
    {
        public RoleModel()
            : base()
        {

        }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }
      
    } 
}