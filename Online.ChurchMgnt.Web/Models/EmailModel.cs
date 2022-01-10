using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online.ChurchMgnt.Web.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }        
    }

    public class ContactModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}