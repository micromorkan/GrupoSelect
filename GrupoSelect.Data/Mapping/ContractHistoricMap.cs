using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace GrupoSelect.Data.Mapping
{
    public class ContractHistoricMap : IEntityTypeConfiguration<ContractHistoric>
    {
        public void Configure(EntityTypeBuilder<ContractHistoric> builder)
        {
            builder.ToTable("ContractHistoric");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.ProposalId).HasColumnName("ProposalId").IsRequired();
            builder.Property(c => c.ContractNum).HasColumnName("ContractNum").IsRequired();
            builder.Property(c => c.Status).HasColumnName("Status").IsRequired();
            builder.Property(c => c.ReprovedReason).HasColumnName("ReprovedReason");
            builder.Property(c => c.ReprovedExplain).HasColumnName("ReprovedExplain");
            builder.Property(c => c.DateRegister).HasColumnName("DateRegister").IsRequired();
            builder.Property(c => c.UserIdRegister).HasColumnName("UserIdRegister").IsRequired();

        }
    }
}
