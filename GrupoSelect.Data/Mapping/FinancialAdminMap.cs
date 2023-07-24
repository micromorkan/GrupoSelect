using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoSelect.Data.Mapping
{
    public class FinancialAdminMap : IEntityTypeConfiguration<FinancialAdmin>
    {
        public void Configure(EntityTypeBuilder<FinancialAdmin> builder)
        {
            builder.ToTable("FinancialAdmin");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();
        }
    }
}
