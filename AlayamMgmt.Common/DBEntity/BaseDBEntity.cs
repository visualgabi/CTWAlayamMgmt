using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.DBEntity
{
    public class BaseDBEntity
    {
        public BaseDBEntity()
        { }


        [Key]
        public int Id { get; set; }        

        public bool Active { get; set; }

        public string CreateUser { get; set; }
                
        public DateTime CreateDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime LastUpdateDateTime { get; set; }

        [Timestamp]
        public byte[] RowTimeStamp { get; set; }
       
    }
}
