using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("RefreshTokens")]
    public class RefreshTokenDBEntity : BaseDBEntity
    {        
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string TokenId { get; set; }

        [Required]
        [MaxLength(250)]
        public string ClientId { get; set; }

         [Required]
        public DateTime IssuedUtc { get; set; }

         [Required]
        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }
    }
}
