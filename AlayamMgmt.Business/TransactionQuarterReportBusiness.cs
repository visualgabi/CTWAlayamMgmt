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
    public class TransactionQuarterReportBusiness : IReportBusiness<TransactionQuarterDBEntity>
    {
        public TransactionQuarterReportBusiness(string aduitUser)           
        { }

        public TransactionQuarterDBEntity Fetch(BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public List<TransactionQuarterDBEntity> FetchAll(BaseCriteria criteria)
        {
            IReportRepository<TransactionQuarterDBEntity> repository = new TransactionQuarterRepositoty();

            return repository.FetchAll(criteria);
        }
    }
}
