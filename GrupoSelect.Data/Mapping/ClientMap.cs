using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Data.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(c => c.CPF).HasColumnName("CPF").HasMaxLength(50).IsRequired();
            builder.Property(c => c.RG).HasColumnName("RG").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Sex).HasColumnName("Sex").HasMaxLength(20).IsRequired();
            builder.Property(c => c.DateBirth).HasColumnName("DateBirth").IsRequired();
            builder.Property(c => c.NaturalFrom).HasColumnName("NaturalFrom").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Nationality).HasColumnName("Nationality").HasMaxLength(50).IsRequired();
            builder.Property(c => c.MaritalStatus).HasColumnName("MaritalStatus").HasMaxLength(50).IsRequired();
            builder.Property(c => c.DateExp).HasColumnName("DateExp").IsRequired();
            builder.Property(c => c.Contact).HasColumnName("Contact").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Profession).HasColumnName("Profession").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Income).HasColumnName("Income").IsRequired();
            builder.Property(c => c.Address).HasColumnName("Address").HasMaxLength(500).IsRequired();
            builder.Property(c => c.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Complement).HasColumnName("Complement").HasMaxLength(500).IsRequired();
            builder.Property(c => c.Cep).HasColumnName("Cep").HasMaxLength(50).IsRequired();
            builder.Property(c => c.City).HasColumnName("City").HasMaxLength(50).IsRequired();
            builder.Property(c => c.State).HasColumnName("State").HasMaxLength(50).IsRequired();
            builder.Property(c => c.DateCreate).HasColumnName("DateCreate").IsRequired();
            builder.Property(c => c.DateUpdate).HasColumnName("DateUpdate");
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();

            builder.HasOne(s => s.User).WithMany().HasForeignKey(s => s.UserId);
        }
    }
}
