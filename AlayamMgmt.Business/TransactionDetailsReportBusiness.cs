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
    public class TransactionDetailsReportBusiness : IReportBusiness<TransactionDetailReportDBEntity>
    {
        public TransactionDetailsReportBusiness(string aduitUser)           
        { }

        public TransactionDetailReportDBEntity Fetch(BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TransactionDetailReportDBEntity> FetchAll(BaseCriteria criteria)
        {
            IReportRepository<TransactionDetailReportDBEntity> repository = new TransactionDetailRepositoty();

            return repository.FetchAll(criteria);
        }
    }
}
