using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IReadOnlyBusiness<TEntity> where TEntity : BaseDBEntity
    {
        TEntity GetById(int id);
        List<TEntity> Get(bool? active);   
    }
}
