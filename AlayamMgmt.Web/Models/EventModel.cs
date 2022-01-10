using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    
    public class EventModel : BaseModel
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
        public int EventTypeId { get; set; }

        public string EventType { get; set; }

        [Required]        
        public int BranchId { get; set; }

        public string Branch { get; set; }        

    }
}