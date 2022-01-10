using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class MarriageAnniversaryRemainderModel : BaseModel
    {
        public int FamilyId { get; set; }
        public string Family { get; set; }

        public string EmailId { get; set; }

        public string HusbandName { get; set; }

        public string WifeName { get; set; }

        public string Phone { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public DateTime MarriageDate { get; set; }

        public bool WishedStatus { get; set; }

        public int? WishedBy { get; set; }

        public string User { get; set; }

        public DateTime? WishedDate { get; set; }

        public int OrganizationId { get; set; }
        public string Organization { get; set; }
    }
}