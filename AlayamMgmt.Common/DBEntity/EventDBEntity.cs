using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("Event")]
    public class EventDBEntity : BaseDBEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public bool IsSpecialEvent { get; set; }

        public int NoOfAdultAttended { get; set; }

        public int NoOfMenAttended { get; set; }

        public int NoOfWomenAttended { get; set; }

        public int NoOfKidsParticipated { get; set; }

        public decimal Offering { get; set; }

        public decimal Expense { get; set; }

        [StringLength(1000)]
        public string SplGuestDetails { get; set; }

        [Required]
        [ForeignKey("EventType"), Column("EventTypeId")]
        public int EventTypeId { get; set; }

        public virtual EventTypeDBEntity EventType { get; set; }

        [Required]
        [ForeignKey("Branch"), Column("BranchId")]
        public int BranchId { get; set; }

        public virtual OrganizationDBEntity Branch { get; set; }

    }
}
