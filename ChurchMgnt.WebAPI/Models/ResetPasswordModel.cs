using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class ResetPasswordModel
    {
        public int UserId { get; set; }

        public string Password { get; set; }

        public string UserEmail { get; set; }
    }
}