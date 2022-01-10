using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class BaseModel
    {
        public BaseModel()
        { }

                
        public int Id { get; set; }        

        public bool Active { get; set; }

        public string CreateUser { get; set; }
                
        public DateTime CreateDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
        
        public byte[] RowTimeStamp { get; set; }
    }
}