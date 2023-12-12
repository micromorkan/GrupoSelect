using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoSelect.Data.Mapping
{
    public class GroupUserMap : IEntityTypeConfiguration<Domain.Entity.GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.ToTable("GroupUser");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.GroupId).HasColumnName("GroupId").IsRequired();
            builder.Property(c => c.UserId).HasColumnName("UserId").IsRequired();

            builder.HasOne(s => s.Group).WithMany().HasForeignKey(s => s.GroupId);
            builder.HasOne(s => s.User).WithMany().HasForeignKey(s => s.UserId);
        }
    }
}

