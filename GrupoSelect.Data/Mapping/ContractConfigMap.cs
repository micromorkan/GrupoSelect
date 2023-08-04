using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace GrupoSelect.Data.Mapping
{
    public class ContractConfigMap : IEntityTypeConfiguration<ContractConfig>
    {
        public void Configure(EntityTypeBuilder<ContractConfig> builder)
        {
            builder.ToTable("ContractConfig");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
            builder.Property(c => c.ContractNum).HasColumnName("ContractNum").IsRequired();
            builder.Property(c => c.DayWeekUpdate).HasColumnName("DayWeekUpdate").IsRequired();
            builder.Property(c => c.HasWeekUpdate).HasColumnName("HasWeekUpdate").IsRequired();
        }
    }
}
