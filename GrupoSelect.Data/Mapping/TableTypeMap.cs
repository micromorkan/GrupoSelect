using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoSelect.Data.Mapping
{
    public class TableTypeMap : IEntityTypeConfiguration<TableType>
    {
        public void Configure(EntityTypeBuilder<TableType> builder)
        {
            builder.ToTable("TableType");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.TableTax).HasColumnName("TableTax").HasMaxLength(50).IsRequired();
            builder.Property(c => c.MembershipFee).HasColumnName("MembershipFee").HasMaxLength(50).IsRequired();
            builder.Property(c => c.RemainingRate).HasColumnName("RemainingRate").HasMaxLength(50).IsRequired();
            builder.Property(c => c.CommissionFee).HasColumnName("CommissionFee").HasMaxLength(50).IsRequired();
            builder.Property(c => c.ManagerFee).HasColumnName("ManagerFee").HasMaxLength(50).IsRequired();
        }
    }
}
