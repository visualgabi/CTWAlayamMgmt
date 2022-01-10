using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class PrintBaseModel
    {
        public string OrgName { get; set; }    

        public string Currency { get; set; }

        public string ChurchMgntURL { get; set; }
    }
}