using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace GrupoSelect.Data.Mapping
{
    public class ContractMap : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.ProposalId).HasColumnName("ProposalId").IsRequired();
            builder.Property(c => c.ContractNum).HasColumnName("ContractNum").IsRequired();
            builder.Property(c => c.Status).HasColumnName("Status").IsRequired();
            builder.Property(c => c.ReprovedReason).HasColumnName("ReprovedReason");
            builder.Property(c => c.ReprovedExplain).HasColumnName("ReprovedExplain");
            builder.Property(c => c.ContractConsultancy).HasColumnName("ContractConsultancy");
            builder.Property(c => c.ContractConsultancyFileType).HasColumnName("ContractConsultancyFileType");
            builder.Property(c => c.ContractFinancialAdmin).HasColumnName("ContractFinancialAdmin");
            builder.Property(c => c.ContractFinancialAdminFileType).HasColumnName("ContractFinancialAdminFileType");
            builder.Property(c => c.VideoAgree).HasColumnName("VideoAgree");
            builder.Property(c => c.VideoAgreeFileType).HasColumnName("VideoAgreeFileType");

            builder.Property(c => c.DateCreate).HasColumnName("DateCreate").IsRequired();
            builder.Property(c => c.DateStatus).HasColumnName("DateStatus");
            builder.Property(c => c.DateAproved).HasColumnName("DateAproved");
            builder.Property(c => c.UserIdAproved).HasColumnName("UserIdAproved");

            builder.HasOne(s => s.Proposal).WithMany().HasForeignKey(s => s.ProposalId);
        }
    }
}
