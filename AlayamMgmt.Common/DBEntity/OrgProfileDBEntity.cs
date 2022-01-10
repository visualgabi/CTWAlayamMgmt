using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class OrgProfileDBEntity : BaseDBEntity
    {
        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public CurrencyDBEntity Currency { get; set; }

        public OrganizationDBEntity Organization { get; set; }
    }
}
