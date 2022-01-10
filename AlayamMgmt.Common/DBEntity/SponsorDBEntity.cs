using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("Sponsor")]
    public class SponsorDBEntity : BaseDBEntity
    {
        public SponsorDBEntity()
        {
           
        }
               

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)] 
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
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        [Required]
        [StringLength(20)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }        

        [Required]
        [StringLength(100)]
        public string EmailId1 { get; set; }
        
        public virtual CountryDBEntity Country { get; set; }

        public virtual StateDBEntity State { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }
    }
}
