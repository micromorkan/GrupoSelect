using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace GrupoSelect.Data.Mapping
{
    public class ProposalMap : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {
            builder.ToTable("Proposal");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.ClientId).HasColumnName("ClientId").IsRequired();
            builder.Property(c => c.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(c => c.ProductTypeName).HasColumnName("ProductTypeName").IsRequired();
            builder.Property(c => c.TableTypeTax).HasColumnName("TableTypeTax").IsRequired();
            builder.Property(c => c.TableTypeFee).HasColumnName("TableTypeFee").IsRequired();
            builder.Property(c => c.TableTypeRate).HasColumnName("TableTypeRate").IsRequired();
            builder.Property(c => c.TableTypeCommission).HasColumnName("TableTypeCommission").IsRequired();
            builder.Property(c => c.TableTypeManager).HasColumnName("TableTypeManager").IsRequired();
            builder.Property(c => c.FinancialAdminName).HasColumnName("FinancialAdminName").IsRequired();
            builder.Property(c => c.CreditValue).HasColumnName("CreditValue").IsRequired();
            builder.Property(c => c.CreditPortionValue).HasColumnName("CreditPortionValue").IsRequired();
            builder.Property(c => c.CreditMembershipValue).HasColumnName("CreditMembershipValue").IsRequired();
            builder.Property(c => c.CreditTotalValue).HasColumnName("CreditTotalValue").IsRequired();

            builder.Property(c => c.DateCreate).HasColumnName("DateCreate").IsRequired();
            builder.Property(c => c.DateChecked).HasColumnName("DateChecked");
            builder.Property(c => c.UserChecked).HasColumnName("UserChecked");
            builder.Property(c => c.Aproved).HasColumnName("Aproved").IsRequired();
            builder.Property(c => c.Status).HasColumnName("Status").IsRequired();

            builder.HasOne(s => s.Client).WithMany().HasForeignKey(s => s.ClientId);
            builder.HasOne(s => s.User).WithMany().HasForeignKey(s => s.UserId);
        }
    }
}
