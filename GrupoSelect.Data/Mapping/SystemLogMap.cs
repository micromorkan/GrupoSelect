﻿using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoSelect.Data.Mapping
{
    public class SystemLogMap : IEntityTypeConfiguration<SystemLog>
    {
        public void Configure(EntityTypeBuilder<SystemLog> builder)
        {
            builder.ToTable("SystemLog");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Action).HasColumnName("Action").IsRequired();
            builder.Property(c => c.Object).HasColumnName("Object").IsRequired();
            builder.Property(c => c.Username).HasColumnName("Username").IsRequired();
            builder.Property(c => c.OriginalValues).HasColumnName("OriginalValues");
            builder.Property(c => c.NewValues).HasColumnName("NewValues");
            builder.Property(c => c.Date).HasColumnName("Date").IsRequired();
        }
    }
}
