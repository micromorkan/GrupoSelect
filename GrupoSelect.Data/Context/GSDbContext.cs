using GrupoSelect.Data.Mapping;
using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Data.Context
{
    public class GSDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public GSDbContext() { }

        public GSDbContext(DbContextOptions<GSDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProfileMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-CMHO5R3\MSSQLSERVER2022;Database=GrupoSelect;User Id=sa;Password=diegoand;encrypt=yes;trustservercertificate=true;");
        }
    }
}
