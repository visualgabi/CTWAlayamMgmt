using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class DatesReportRequestModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class OrgDatesReportRequestModel : DatesReportRequestModel
    {
        public int OrganizationId { get; set; }
    }
}