using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoSelect.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(200).IsRequired();
            builder.Property(c => c.Cnpj).HasColumnName("Cnpj").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(200).IsRequired();
            builder.Property(c => c.Representation).HasColumnName("Representation").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Profile).HasColumnName("Profile").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Login).HasColumnName("Login").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Password).HasColumnName("Password").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();
            builder.Property(c => c.BranchWithoutAdm).HasColumnName("BranchWithoutAdm").IsRequired();
        }
    }
}
