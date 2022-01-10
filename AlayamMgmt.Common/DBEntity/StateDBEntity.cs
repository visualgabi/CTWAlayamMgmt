using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    [Table("State")]
    public class StateDBEntity : LookupDBEntity
    {
        public StateDBEntity()
        {
            Country = new CountryDBEntity();
        }

        [ForeignKey("Country"), Column("CountryId")]
        public int CountryId { get; set; }

        public virtual CountryDBEntity Country { get; set; }
    }
}
