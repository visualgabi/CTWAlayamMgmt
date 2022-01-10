using AlayamMgmt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class GroupBasicModel
    {
        public int OrderId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public EmailGroup Type { get; set; }

        public string TypeInString { get; set; }
    }
}