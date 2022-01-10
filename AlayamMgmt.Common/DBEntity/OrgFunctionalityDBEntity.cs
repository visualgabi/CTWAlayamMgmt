using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class OrgFunctionalityDBEntity : BaseDBEntity
    {
        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int FunctionalityId { get; set; }

        public FunctionalityDBEntity Functionality { get; set; }

        public OrganizationDBEntity Organization { get; set; }
    }
}
