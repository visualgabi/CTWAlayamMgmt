using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class AccountViewModels
    {
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =
            "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [DataType(DataType.Password)]        
        [Compare("Password", ErrorMessage =
            "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]        
        public string FirstName { get; set; }

        [Required]        
        public string LastName { get; set; }

        [Required]        
        public int RoleId { get; set; }
                
        public int OrgId { get; set; }
        
    }

    public class RoleViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }


    public class UserViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }       

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }

    //public class RoleViewModel
    //{
    //    [Required]
    //    public int Id { get; set; }

    //    [Required]
    //    public string Name { get; set; }        
    //}

    public class UserRoleViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}