using GrupoSelect.Data.Mapping;
using GrupoSelect.Domain.Entity;
using GrupoSelect.Domain.Interface;
using GrupoSelect.Domain.Models;
using GrupoSelect.Domain.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace GrupoSelect.Data.Context
{
    public class GSDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private UserSession _userSession;

        public DbSet<LogError> ErrorLogs { get; set; }
        public DbSet<LogSystem> SystemLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialAdmin> FinancialAdmins { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<TableType> TableTypes { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractHistoric> ContractHistorics { get; set; }
        public DbSet<ContractConfig> ContractConfigs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }


        public GSDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GSDbContext(DbContextOptions<GSDbContext> options, IConfiguration configuration, ISessionProvider sessionProvider) : base(options)
        {
            _configuration = configuration;
            _userSession = sessionProvider.Get();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new LogSystemMap());
            modelBuilder.ApplyConfiguration(new LogErrorMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new FinancialAdminMap());
            modelBuilder.ApplyConfiguration(new ProductTypeMap());
            modelBuilder.ApplyConfiguration(new TableTypeMap());
            modelBuilder.ApplyConfiguration(new CreditMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProposalMap());
            modelBuilder.ApplyConfiguration(new ContractMap());
            modelBuilder.ApplyConfiguration(new ContractHistoricMap());
            modelBuilder.ApplyConfiguration(new ContractConfigMap());
            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new GroupUserMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = _configuration.GetConnectionString(Constants.SYSTEM_CONN_STRING);

            optionsBuilder.UseLazyLoadingProxies().ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning)).UseMySql(connString, ServerVersion.AutoDetect(connString));
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-CMHO5R3\MSSQLSERVER2022;Database=GrupoSelect;User Id=sa;Password=diegoand;encrypt=yes;trustservercertificate=true;");
        }

        public override int SaveChanges()
        {
            if (Convert.ToBoolean(_configuration.GetSection(Constants.SYSTEM_SETTINGS)[Constants.SYSTEM_SETTINGS_REGISTERSYSTEMLOG]))
            {
                var entries = DetectEntries();
                List<LogSystem> logs = new List<LogSystem>(entries.Count());

                foreach (var entry in entries)
                {
                    LogSystem newLog = GetLog(entry);

                    if (newLog != null && !Constants.SYSTEM_IGNORE_AUDIT_TABLES.Contains(newLog.Object))
                    {
                        logs.Add(newLog);
                    }
                }

                foreach (var item in logs)
                {
                    this.Entry(item).State = EntityState.Added;
                }
            }

            return base.SaveChanges();
        }

        private IEnumerable<EntityEntry> DetectEntries()
        {
            return ChangeTracker.Entries().Where(e => (e.State == EntityState.Modified ||
                                                        e.State == EntityState.Added ||
                                                        e.State == EntityState.Deleted) &&
                                                        e.Entity.GetType() != typeof(LogSystem));
        }

        private LogSystem GetLog(EntityEntry entry)
        {
            LogSystem returnValue = null;

            if (entry.State == EntityState.Added)
            {
                returnValue = GetInsertLog(entry);
            }
            else if (entry.State == EntityState.Modified)
            {
                returnValue = GetUpdateLog(entry);
            }
            else if (entry.State == EntityState.Deleted)
            {
                returnValue = GetDeleteLog(entry);
            }

            return returnValue;
        }

        private LogSystem GetInsertLog(EntityEntry entry)
        {
            LogSystem log = new LogSystem();

            log.Action = Constants.SYSTEM_LOG_INSERT;
            log.Object = entry.Entity.GetType().Name;
            log.Username = _userSession.UserName;
            log.OriginalValues = null;
            log.NewValues = JsonSerializer.Serialize(entry.Entity);

            return log;
        }

        private LogSystem GetDeleteLog(EntityEntry entry)
        {
            LogSystem log = new LogSystem();

            log.Action = Constants.SYSTEM_LOG_DELETE;
            log.Object = entry.Entity.GetType().Name;
            log.Username = _userSession.UserName;
            log.OriginalValues = JsonSerializer.Serialize(entry.Entity);
            log.NewValues = null;

            return log;
        }

        private LogSystem GetUpdateLog(EntityEntry entry)
        {
            object originalValue = null;

            if (entry.OriginalValues != null)
            {
                originalValue = entry.OriginalValues.ToObject();
            }
            else
            {
                originalValue = entry.GetDatabaseValues().ToObject();
            }

            LogSystem log = new LogSystem();

            log.Action = Constants.SYSTEM_LOG_UPDATE;
            log.Object = entry.Entity.GetType().BaseType.Name;
            log.Username = _userSession.UserName;
            log.OriginalValues = JsonSerializer.Serialize(originalValue);
            log.NewValues = JsonSerializer.Serialize(entry.Entity);

            return log;
        }
    }
}
