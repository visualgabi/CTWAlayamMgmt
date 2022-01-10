using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Business
{
    public class TransactionMonthlyReportBusiness : IReportBusiness<TransactionMonthlyDBEntity>
    {
        public TransactionMonthlyReportBusiness(string aduitUser)           
        { }

        public TransactionMonthlyDBEntity Fetch(BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TransactionMonthlyDBEntity> FetchAll(BaseCriteria criteria)
        {
            IReportRepository<TransactionMonthlyDBEntity> repository = new TransactionMonthlyRepositoty();

            return repository.FetchAll(criteria);
        }
    }
}
