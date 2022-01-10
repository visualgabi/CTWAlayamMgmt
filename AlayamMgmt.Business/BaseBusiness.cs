using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Common.Interface.Business;
using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.BusinessLogic
{
    public class BaseBusiness<TEntity> : IBusiness<TEntity> where TEntity : BaseDBEntity
    {
        public IUnitOfWork<TEntity> _uow;        
        
        public BaseBusiness(string auditUser)
        {
            if (_uow == null)
            {
                _uow = new UnitOfWork<TEntity>(auditUser);
            }
        }

        public BaseBusiness(IUnitOfWork<TEntity> uow)
        {
            _uow = uow;
        }

        public void Save(TEntity entity)
        {
            _uow.GenericRepositoryObj.Save(entity);
            _uow.Commit();         
        }

        public byte[] Enable(int id, bool active)
        {
            _uow.GenericRepositoryObj.Enable(id, active);
            _uow.Commit();

            TEntity entity = _uow.GenericRepositoryObj.GetByID(id);
            return entity.RowTimeStamp;

        }

        public TEntity GetById(int Id)
        {
            return _uow.GenericRepositoryObj.GetByID(Id);
        }

        public virtual List<TEntity> Get(bool? active)
        {
            if (active != null)
                return _uow.GenericRepositoryObj.Get(x => x.Active == active.Value).ToList();
            else
                return _uow.GenericRepositoryObj.Get().ToList();
        }
    }
}
