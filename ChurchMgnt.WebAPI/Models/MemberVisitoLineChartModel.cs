using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class MemberVisitoLineChartModel
    {
        public MemberVisitoLineChartModel()
        {
            Members = new List<LineChartModel>();
            Visitors = new List<LineChartModel>();
            Labels = new List<string>();
            Series = new List<string>();
            MemberCount = new List<int>();
            VisitorCount = new List<int>();            
        }
       
        public List<string> Labels { get; set; }
        public List<string> Series { get; set; }
        public List<LineChartModel> Members { get; set; }
        public List<int> MemberCount { get; set; }
        public List<LineChartModel> Visitors { get; set; }
        public List<int> VisitorCount { get; set; }
        public List<int> MemberAndVisitorCount { get; set; }    
    }

    
}