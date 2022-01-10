using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Business
{
    public interface IEditableBusiness<TEntity> where TEntity : BaseDBEntity
    {
        void Save(TEntity entity);
        byte[] Enable(int id, bool active);
    }
}
