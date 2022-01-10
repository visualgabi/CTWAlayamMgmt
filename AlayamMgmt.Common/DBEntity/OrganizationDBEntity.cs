namespace AlayamMgmt.Common.DBEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

    [Table("Organization")]
    public partial class OrganizationDBEntity : BaseDBEntity
    {
        public OrganizationDBEntity()
        {            
            FiscalYears = new HashSet<OrgFiscalYearDBEntity>();   
        }
               

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)] 
        public string Name { get; set; }

        [StringLength(500)]
        public string Discription { get; set; }
               
        public int? DenominationId { get; set; }

        [Required]
        public int EthnicOriginId { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Required]
        [ForeignKey("PrimaryLanguage"), Column("PrimaryLanguageId")]
        public int PrimaryLanguageId { get; set; }

        [ForeignKey("SecondaryLanguage"), Column("SecondaryLanguageId")]
        [Required]
        public int SecondaryLanguageId { get; set; }

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

        [StringLength(15)]
        public string Phone2 { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }

        [StringLength(100)]
        public string EmailId2 { get; set; }
        
        [StringLength(250)]
        public string TaxId { get; set; }

        [ForeignKey("Currency"), Column("CurrencyId")]        
        public int? CurrencyId { get; set; }

        public virtual CurrencyDBEntity Currency { get; set; }
     
        [ForeignKey("FiscalYearStartAndEndMonth"), Column("FiscalYearStartAndEndMonthId")]
        public int? FiscalYearStartAndEndMonthId { get; set; }

        public virtual FiscalYearStartAndEndMonthDBEntity FiscalYearStartAndEndMonth { get; set; }
        
        public virtual DenominationDBEntity Denomination { get; set; }

        public virtual EthnicOriginDBEntity EthnicOrigin { get; set; }
                
        public virtual LanguageDBEntity PrimaryLanguage { get; set; }
        
        public virtual LanguageDBEntity SecondaryLanguage { get; set; }

        public virtual CountryDBEntity Country { get; set; }

        public virtual StateDBEntity State { get; set; }
       
        public virtual ICollection<OrgFiscalYearDBEntity> FiscalYears { get; set; }

        [ForeignKey("Parent"), Column("ParentId")]
        public int? ParentId { get; set; }

        public virtual OrganizationDBEntity Parent { get; set; }        
        
    }
}