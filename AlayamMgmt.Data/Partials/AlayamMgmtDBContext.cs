using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Context
{
    public partial class AlayamMgmtDBContext : DbContext
    {
        public string AuditUser { get; set; }

        public AlayamMgmtDBContext(string auditUser)
            : base("name=AlayamDBContext")
        {
            AuditUser = auditUser;
            //this.Configuration.LazyLoadingEnabled = true;        
        }

        public override int SaveChanges()
        {
            //if AuditUser is empty, throw an exception         

            ChangeTracker.DetectChanges();

            ObjectContext ctx = ((IObjectContextAdapter)this).ObjectContext;

            List<ObjectStateEntry> objectStateEntryList =
                ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added
                                                           | EntityState.Modified)
                .ToList();

            foreach (ObjectStateEntry entry in objectStateEntryList)
            {
                CurrentValueRecord entryValues = entry.CurrentValues;

                if (ColumnExists(entryValues, "LastUpdateUser"))
                    entryValues.SetString(entryValues.GetOrdinal("LastUpdateUser"), AuditUser);

                if (ColumnExists(entryValues, "LastUpdateDateTime"))
                    entryValues.SetDateTime(entryValues.GetOrdinal("LastUpdateDateTime"), DateTime.UtcNow);

                if (entry.State == EntityState.Added)
                {
                    if (ColumnExists(entryValues, "CreateUser"))
                        entryValues.SetString(entryValues.GetOrdinal("CreateUser"), AuditUser);

                    if (ColumnExists(entryValues, "CreateDateTime"))
                        entryValues.SetDateTime(entryValues.GetOrdinal("CreateDateTime"), DateTime.UtcNow);
                }
            }

            return base.SaveChanges();
        }

        private bool ColumnExists(CurrentValueRecord entryValues, string columnName)
        {
            try
            {
                if (entryValues.GetOrdinal(columnName) > 0) return true;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
