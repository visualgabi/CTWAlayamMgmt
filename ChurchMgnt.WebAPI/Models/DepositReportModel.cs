using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class DepositReportRequestModel
    {
        public int OrganizationId { get; set; }
        public DateTime DepositStartDate { get; set; }
        public DateTime DepositEndDate { get; set; }
        public int? AccountId { get; set; }
        public int? UserId { get; set; }
        public int? OrderById { get; set; }
    }
}