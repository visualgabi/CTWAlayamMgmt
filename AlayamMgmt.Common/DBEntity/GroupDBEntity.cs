using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("Group")]
    public class GroupDBEntity : OrganizationLookupDBEntity
    {
        //public GroupDBEntity()
        //{
        //    FamilyMembers = new HashSet<FamilyMemberDBEntity>();
        //}

        //public virtual ICollection<FamilyMemberDBEntity> FamilyMembers { get; set; }

    }
}
