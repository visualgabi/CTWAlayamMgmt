using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class OrganizationModel : BaseModel
    {
        public OrganizationModel()
        {
           
        }
               

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Discription { get; set; }
                
        public int? DenominationId { get; set; }
        
        public string Denomination { get; set; }

        [Required]
        public int EthnicOriginId { get; set; }
        public string EthnicOrigin { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Required]       
        public int PrimaryLanguageId { get; set; }
        
        public string PrimaryLanguage { get; set; }
                
        [Required]
        public int SecondaryLanguageId { get; set; }
        public string SecondaryLanguage { get; set; }

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

        [Required]
        public int StateId { get; set; }
        public string State { get; set; }

         [Required]
        public int CountryId { get; set; }
        public string Country { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [StringLength(15)]
        public string Phone2 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }

        [StringLength(100)]
        public string EmailId2 { get; set; }   
                
        public int? ParentId { get; set; }

        [StringLength(250)]
        public string TaxId { get; set; }

        [Required]
        public int? CurrencyId { get; set; }

        [StringLength(100)]
        public string Currency { get; set; }

       
        public int? FiscalYearStartAndEndMonthId { get; set; }

        public virtual string FiscalYearStartAndEndMonth { get; set; }
    }

    public class OrganizationBasicModel : BaseModel
    {
        public OrganizationBasicModel()
        {

        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
                
        [StringLength(100)]
        public string Website { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }
                
        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public string State { get; set; }

        [Required]        
        public string Country { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }
        
        [StringLength(250)]
        public string TaxId { get; set; }

        [StringLength(100)]
        public string Currency { get; set; }        
    }
}