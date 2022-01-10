using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlayamMgmt.Web.Models
{
    public class LogoModel
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
    }
}