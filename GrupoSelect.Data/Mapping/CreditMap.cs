using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace GrupoSelect.Data.Mapping
{
    public class CreditMap : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.ToTable("Credit");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.ProductTypeId).HasColumnName("ProductTypeId").IsRequired();
            builder.Property(c => c.TableTypeId).HasColumnName("TableTypeId").IsRequired();
            builder.Property(c => c.FinancialAdminId).HasColumnName("FinancialAdminId").IsRequired();
            builder.Property(c => c.Months).HasColumnName("Months").IsRequired();
            builder.Property(c => c.CreditValue).HasColumnName("CreditValue").IsRequired();
            builder.Property(c => c.PortionValue).HasColumnName("PortionValue").IsRequired();
            builder.Property(c => c.MembershipValue).HasColumnName("MembershipValue").IsRequired();
            builder.Property(c => c.TotalValue).HasColumnName("TotalValue").IsRequired();
            builder.Property(c => c.DateCreate).HasColumnName("DateCreate").IsRequired();
            builder.Property(c => c.DateUpdate).HasColumnName("DateUpdate");

            //builder.HasOne(e => e.ProductType).WithOne().HasForeignKey<ProductType>(e => e.Id).IsRequired();

            builder.HasOne(s => s.ProductType).WithMany().HasForeignKey(s => s.ProductTypeId);
            builder.HasOne(s => s.TableType).WithMany().HasForeignKey(s => s.TableTypeId);
            builder.HasOne(s => s.FinancialAdmin).WithMany().HasForeignKey(s => s.FinancialAdminId);
        }
    }
}
