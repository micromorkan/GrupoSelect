﻿using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrupoSelect.Data.Mapping
{
    public class LogErrorMap : IEntityTypeConfiguration<LogError>
    {
        public void Configure(EntityTypeBuilder<LogError> builder)
        {
            builder.ToTable("LogError");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Method).HasColumnName("Method");
            builder.Property(c => c.Message).HasColumnName("Message").IsRequired();
            builder.Property(c => c.Object).HasColumnName("Object");
            builder.Property(c => c.Username).HasColumnName("Username");
            builder.Property(c => c.Date).HasColumnName("Date").IsRequired();
        }
    }
}