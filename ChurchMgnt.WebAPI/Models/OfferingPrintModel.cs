using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class OfferingPrintModel : PrintBaseModel
    {
        
        public decimal TotalOffering { get; set; }

        public OfferingReportFilter Filter { get; set; }

        public List<Summary> SummaryBySourceTypes { get; set; }

        public List<Summary> SummaryByPaymentTypes { get; set; }

        public List<Summary> SummaryByOfferingTypes { get; set; }

        public List<Summary> summaryByFundTypes { get; set; }

        public List<OrgOfferingModel> Offerings { get; set; }

        
    }   
    public class OfferingReportFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Source { get; set; }

        public string OfferingType { get; set; }

        public string FundType { get; set; }

        public string OrderBy { get; set; }
    }
}