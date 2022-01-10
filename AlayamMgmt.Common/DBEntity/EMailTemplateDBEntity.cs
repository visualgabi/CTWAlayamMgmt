using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("EmailTemplate")]
    public class EMailTemplateDBEntity : OrganizationLookupDBEntity
    {
       public EMailTemplateDBEntity()
            : base()
       {
        
       }

       public string Content { get; set; }
    }
}
