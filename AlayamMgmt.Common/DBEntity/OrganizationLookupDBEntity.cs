using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class OrganizationLookupDBEntity : LookupDBEntity
    {
        [Required]
        [ForeignKey("Organization"), Column("OrganizationId")]
        public int OrganizationId { get; set; }

        public virtual OrganizationDBEntity Organization { get; set; }
    }
}
