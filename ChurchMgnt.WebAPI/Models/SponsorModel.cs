using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class SponsorModel : BaseModel
    {
        [Required]
        [StringLength(100)]        
        public string Name { get; set; }       

        [StringLength(100)]
        public string Website { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }

        public int OrganizationId { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string Organization { get; set; }
    }


    public class SponsorBasicModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }
        
        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }

        public string Country { get; set; }

        public string State { get; set; }
        
    }
}