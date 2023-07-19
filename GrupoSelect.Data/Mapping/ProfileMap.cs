using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoSelect.Data.Mapping
{
    public class ProfileMap : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(200).IsRequired();
            builder.Property(c => c.Active).HasColumnName("Active").IsRequired();
        }
    }
}
