
using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Data
{
    public interface IGenericRepository<TEntity> : IGenericReadOnlyRepository<TEntity>, IGenericEditableRepository<TEntity> where TEntity : BaseDBEntity
    {  
    }
}
