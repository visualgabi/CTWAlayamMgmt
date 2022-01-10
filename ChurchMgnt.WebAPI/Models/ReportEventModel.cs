using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class EventReportRequestModel
    {
        public int OrganizationId { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int? EventTypeId { get; set; }
        public int? SplEventTypeId { get; set; }
        public int? OrderById { get; set; }
    }
}