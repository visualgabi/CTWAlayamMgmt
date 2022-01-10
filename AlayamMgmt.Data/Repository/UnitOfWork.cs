
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlayamMgmt.Common.Interface.Data;
using AlayamMgmt.Common.DBEntity;
using AlayamMgmt.Data.Context;


namespace AlayamMgmt.Data.Repository
{
    public partial class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseDBEntity
    {
        private DbContext _context;

        public UnitOfWork(string auditUser)
        {
            _context = new AlayamMgmtDBContext(auditUser);        
            _context.Configuration.ValidateOnSaveEnabled = true;
        }

        public UnitOfWork(DbContext dbContext)
        {

            _context = dbContext;
        }

        public DbContext Context
        {
            get
            {
                return _context;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        private IGenericRepository<TEntity> _iGenericRepositoryObj;
        public IGenericRepository<TEntity> GenericRepositoryObj
        {
            set
            {
                _iGenericRepositoryObj = value;
            }
            get
            {
                if (this._iGenericRepositoryObj == null)
                {
                    this._iGenericRepositoryObj = new GenericRepository<TEntity>(this.Context);
                }
                return _iGenericRepositoryObj;
            }
        }

        private IGenericRepository<GroupMemberDBEntity> _iGroupMemberRepositoryObj;
        public IGenericRepository<GroupMemberDBEntity> GroupMemberRepositoryObj
        {
            set
            {
                _iGroupMemberRepositoryObj = value;
            }
            get
            {
                if (this._iGroupMemberRepositoryObj == null)
                {
                    this._iGroupMemberRepositoryObj = new GenericRepository<GroupMemberDBEntity>(this.Context);
                }
                return _iGroupMemberRepositoryObj;
            }
        }
    }
   }
