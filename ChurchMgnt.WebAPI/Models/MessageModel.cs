using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ChurchMgnt.WebAPI.Models
{
    public class MessageModel : BaseModel
    {
        [Required]
        [StringLength(4000)]
        public string Message { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        public string Organization { get; set; }

        public int MessageTypeId { get; set; }

        public string MessageType { get; set; }
    }
}