using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Data
{
    public interface IGenericEditableRepository<TEntity> where TEntity : BaseDBEntity
    {
        void Save(TEntity entity);
        void Enable(int id, bool active);
    }
}
