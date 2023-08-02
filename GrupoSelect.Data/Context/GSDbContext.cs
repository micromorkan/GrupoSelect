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

        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialAdmin> FinancialAdmins { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<TableType> TableTypes { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

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
            modelBuilder.ApplyConfiguration(new SystemLogMap());
            modelBuilder.ApplyConfiguration(new ErrorLogMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
            modelBuilder.ApplyConfiguration(new FinancialAdminMap());
            modelBuilder.ApplyConfiguration(new ProductTypeMap());
            modelBuilder.ApplyConfiguration(new TableTypeMap());
            modelBuilder.ApplyConfiguration(new CreditMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProposalMap());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = _configuration.GetConnectionString(Constants.SYSTEM_CONN_STRING);

            optionsBuilder.UseLazyLoadingProxies().ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning)).UseSqlServer(connString);
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-CMHO5R3\MSSQLSERVER2022;Database=GrupoSelect;User Id=sa;Password=diegoand;encrypt=yes;trustservercertificate=true;");
        }

        public override int SaveChanges()
        {
            if (Convert.ToBoolean(_configuration.GetSection(Constants.SYSTEM_SETTINGS)[Constants.SYSTEM_SETTINGS_REGISTERSYSTEMLOG]))
            {
                var entries = DetectEntries();
                List<SystemLog> logs = new List<SystemLog>(entries.Count());

                foreach (var entry in entries)
                {
                    SystemLog newLog = GetLog(entry);

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
                                                        e.Entity.GetType() != typeof(SystemLog));
        }

        private SystemLog GetLog(EntityEntry entry)
        {
            SystemLog returnValue = null;

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

        private SystemLog GetInsertLog(EntityEntry entry)
        {
            SystemLog log = new SystemLog();

            log.Action = Constants.SYSTEM_LOG_INSERT;
            log.Object = entry.Entity.GetType().Name;
            log.Username = _userSession.UserName;
            log.OriginalValues = null;
            log.NewValues = JsonSerializer.Serialize(entry.Entity);

            return log;
        }

        private SystemLog GetDeleteLog(EntityEntry entry)
        {
            SystemLog log = new SystemLog();

            log.Action = Constants.SYSTEM_LOG_DELETE;
            log.Object = entry.Entity.GetType().Name;
            log.Username = _userSession.UserName;
            log.OriginalValues = JsonSerializer.Serialize(entry.Entity);
            log.NewValues = null;

            return log;
        }

        private SystemLog GetUpdateLog(EntityEntry entry)
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

            SystemLog log = new SystemLog();

            log.Action = Constants.SYSTEM_LOG_UPDATE;
            log.Object = entry.Entity.GetType().BaseType.Name;
            log.Username = _userSession.UserName;
            log.OriginalValues = JsonSerializer.Serialize(originalValue);
            log.NewValues = JsonSerializer.Serialize(entry.Entity);

            return log;
        }
    }
}
