using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("EmailContent")]
    public class EmailContentDBEntity : BaseDBEntity
    {
        public EmailContentDBEntity()
        {
           
        }

        [ForeignKey("EmailType"), Column("EamilTypeId")]
        public int EamilTypeId { get; set; }

         [ForeignKey("Organization"), Column("OrganizationId")]
        public int? OrganizationId { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }               

        [ForeignKey("EamilTypeId")]
        public virtual EmailTypeDBEntity EmailType { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual OrganizationDBEntity Organization { get; set; }
    }
}
