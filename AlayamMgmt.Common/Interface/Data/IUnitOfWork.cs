using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Common.Interface.Data
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : BaseDBEntity
    {
        void Commit();
        IGenericRepository<TEntity> GenericRepositoryObj { set; get; }

        IGenericRepository<GroupMemberDBEntity> GroupMemberRepositoryObj { set; get; }
    }
}
