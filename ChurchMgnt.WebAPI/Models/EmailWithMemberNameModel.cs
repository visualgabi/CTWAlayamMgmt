using AlayamMgmt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class EmailWithMemberNameModel
    {
        /// <summary>
        /// primary key for order by
        /// </summary>
        public int PKId {get; set;}

        /// <summary>
        /// It could be family member id or family id based on the entitytype
        /// </summary>
        public int Id { get; set; }

        public string EmailId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }        

        public bool EmailSend { get; set; }
    }
}