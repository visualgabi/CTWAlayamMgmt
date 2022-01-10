using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{    
    [Table("FiscalYearStartAndEndMonth")]
    public class FiscalYearStartAndEndMonthDBEntity : LookupDBEntity
    {
        public FiscalYearStartAndEndMonthDBEntity()
            : base()
        {

        }
    }
}
