using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class AccountModel : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Number { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
        
        public int AccountTypeId { get; set; }
        
        public int BankId { get; set; }

        public string AccountType { get; set; }

        public string Bank { get; set; }
    }
}