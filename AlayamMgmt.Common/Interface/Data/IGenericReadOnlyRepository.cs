using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Data
{
    public interface IGenericReadOnlyRepository<TEntity> where TEntity : BaseDBEntity
    {
        TEntity GetByID(object id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
            string includeProperties = "");


        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           string[] includePaths = null,
           int? page = null,
           int? pageSize = null,
           params SortExpression<TEntity>[] sortExpressions);

        IEnumerable<TEntity> GetWithIncludeAndNoTracking(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IQueryable<TEntity>> orderBy = null,
       params Expression<Func<TEntity, object>>[] includeProperties);

    }
}
