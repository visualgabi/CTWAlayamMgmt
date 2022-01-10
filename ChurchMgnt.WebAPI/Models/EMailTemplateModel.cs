using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class EMailTemplateModel : OrganizationLookupModel
    {
        public EMailTemplateModel()
            : base()
       {
        
       }

       [Required]
       public string Content { get; set; }
    }
}