using AlayamMgmt.Common.DBEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlayamMgmt.Data.Context
{
    public partial class AlayamMgmtDBContext : DbContext
    {
        public AlayamMgmtDBContext()
            : base("name=AlayamDBContext")
        {
            //Database.SetInitializer(new icm)
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<RoleDBEntity> Roles { get; set; }
        public virtual DbSet<UserDBEntity> Users { get; set; }
        public virtual DbSet<UserRoleDBEntity> UserRoles { get; set; }
        public virtual DbSet<OrganizationDBEntity> Organizations { get; set; }        
        public virtual DbSet<CountryDBEntity> Countries { get; set; }
        public virtual DbSet<EmailContentDBEntity> EmailContents { get; set; }
        public virtual DbSet<EmailTypeDBEntity> EmailTypes { get; set; }
        public virtual DbSet<FamilyDBEntity> Families { get; set; }
        public virtual DbSet<FamilyMemberDBEntity> FamilyMembers { get; set; }
        public virtual DbSet<MembershipStatusDBEntity> MembershipStatuses { get; set; }
        public virtual DbSet<CurrencyDBEntity> Currencies { get; set; }
        public virtual DbSet<FundTypeDBEntity> FundTypes { get; set; }
        public virtual DbSet<OfferingTypeDBEntity> OfferingTypes { get; set; }
        public virtual DbSet<OrgFiscalYearDBEntity> OrgFiscalYears { get; set; }
        public virtual DbSet<FiscalYearDBEntity> FiscalYears { get; set; }
        public virtual DbSet<OrgFiscalYearBudgetDBEntity> OrgFiscalYearBudgets { get; set; }
        public virtual DbSet<FamilyPledgeDBEntity> FamilyPledges { get; set; }
        public virtual DbSet<OfferingDBEntity> Offerings { get; set; }
        public virtual DbSet<ExpenseTypeDBEntity> ExpenseTypes { get; set; }
        public virtual DbSet<SubExpenseTypeDBEntity> SubExpenseTypes { get; set; }
        public virtual DbSet<ExpenseDBEntity> Expenses { get; set; }
        public virtual DbSet<SponsorDBEntity> Sponsors { get; set; }
        public virtual DbSet<ClientDBEntity> Clients { get; set; }
        public virtual DbSet<RefreshTokenDBEntity> RefreshTokens { get; set; }
        public virtual DbSet<EventTypeDBEntity> EventTypes { get; set; }
        public virtual DbSet<EventDBEntity> Events { get; set; }
        public virtual DbSet<BankDBEntity> Banks { get; set; }
        public virtual DbSet<AccountTypeDBEntity> AccountTypes { get; set; }
        public virtual DbSet<AccountDBEntity> Accounts { get; set; }
        public virtual DbSet<DepositDBEntity> Deposits { get; set; }
        public virtual DbSet<OpeningBalanceDBEntity> OpeningBalances { get; set; }
        public virtual DbSet<RelationshipDBEntity> Relationships { get; set; }
        public virtual DbSet<FiscalYearStartAndEndMonthDBEntity> FiscalYearStartAndEndMonths { get; set; }
        public virtual DbSet<TaxFilingDBEntity> TaxFilings { get; set; }
        public virtual DbSet<ReportedIssueDBEntity> ReportedIssues { get; set; }
        public virtual DbSet<IssueStatusDBEntity> IssueStatuses { get; set; }
        public virtual DbSet<EMailTemplateDBEntity> EMailTemplateDBEntites { get; set; }
        public virtual DbSet<SMTPSettingDBEntity> OrgSMTPDetailsDBEntites { get; set; }
        public virtual DbSet<GroupDBEntity> GroupDBEntites { get; set; }
        public virtual DbSet<GroupMemberDBEntity> GroupMemberDBEntites { get; set; }
        public virtual DbSet<MessageDBEntity> MessageDBEntities { get; set; }
        public virtual DbSet<BirthdayRemainderDBEntity> BirthdayRemainderDBEntities { get; set; }
        public virtual DbSet<MarriageAnniversaryRemainderDBEntity> MarriageAnniversaryRemainderDBEntities { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BirthdayRemainderDBEntity>().Ignore(t => t.Active);
            modelBuilder.Entity<MarriageAnniversaryRemainderDBEntity>().Ignore(t => t.Active);
        }
    }
}
