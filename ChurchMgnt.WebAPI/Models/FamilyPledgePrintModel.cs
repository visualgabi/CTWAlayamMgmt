using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class FamilyPledgePrintModel : PrintBaseModel
    {   
        public decimal TotalOffering { get; set; }

        public FamilyPledgeReportFilter Filter { get; set; }                

        public List<Summary> SummaryByPaymentTypes { get; set; }

        public List<Summary> SummaryByOfferingTypes { get; set; }

        public List<Summary> summaryByFundTypes { get; set; }

        public List<OrgOfferingModel> Offerings { get; set; }        
    }
    public class FamilyPledgeReportFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
    }
}
