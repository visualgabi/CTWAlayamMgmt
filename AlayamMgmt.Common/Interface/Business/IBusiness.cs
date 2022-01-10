using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IBusiness<TEntity> : IEditableBusiness<TEntity>, IReadOnlyBusiness<TEntity> where TEntity : BaseDBEntity
    {
    }
}
