using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchMgnt.WebAPI.Models
{
    public class ExpenseReportRequestModel
    {
        public ExpenseReportRequestModel()
        { }

        public int OrganizationId { get; set; }
        public DateTime ExpenseStartDate {get; set;}
        public DateTime ExpenseEndDate {get; set;}
        public int? ExpenseTypeId { get; set; }
        public int? OrderById { get; set; }
        public int? AccountId { get; set; }
        
        //public List<int> ExpenseTypes {get;set;}
        //public List<int> SubExpenseTypes {get;set;}
        //public List<int> PaymentTypes { get; set; }
    }
}