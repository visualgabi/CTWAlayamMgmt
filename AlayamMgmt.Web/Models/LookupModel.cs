using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class LookupModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }

    public class StateModel : LookupModel
    {
        public int CountryId { get; set; }        
    }

    public class OrganizationLookupModel : LookupModel
    {
        [Required]        
        public int OrganizationId { get; set; }

        [StringLength(250)]
        public string Organization { get; set; }
    }
}