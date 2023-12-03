using GrupoSelect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrupoSelect.Data.Mapping
{
    public class GroupMap
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Name).HasColumnName("Name").IsRequired();

        }
    }
}

