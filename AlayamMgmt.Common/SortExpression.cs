using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AlayamMgmt.Common.DBEntity;

namespace AlayamMgmt.Common
{

    public class SortExpression<TEntity> where TEntity : BaseDBEntity
    {
        public SortExpression(Expression<Func<TEntity, object>> sortBy, ListSortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public Expression<Func<TEntity, object>> SortBy { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }

    //public class SortExpression<TEntity> where TEntity : BaseDBEntity
    //{


    ////    public SortExpression(Expression<Func<TEntity, object>> sortBy, ListSortDirection sortDirection)
    ////    {
    ////        SortBy = sortBy;
    ////        SortDirection = sortDirection;
    ////    }

    ////    public Expression<Func<TEntity, object>> SortBy { get; set; }
    ////    public ListSortDirection SortDirection { get; set; }

    //public SortExpression(Func<IQueryable<TEntity>, IQueryable<TEntity>> sortBy, ListSortDirection sortDirection)
    //{
    //    SortBy = sortBy;
    //    SortDirection = sortDirection;
    //}

    //public Func<IQueryable<TEntity>, IQueryable<TEntity>> SortBy { get; set; }
    //public ListSortDirection SortDirection { get; set; }

    // }
}
