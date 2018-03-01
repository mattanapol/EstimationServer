using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class MaterialGroupEntityTypeConfiguration : IEntityTypeConfiguration<MaterialGroupDb>
    {
        public void Configure(EntityTypeBuilder<MaterialGroupDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.GroupCode).IsRequired();
            builder.Property(m => m.ProjectId).IsRequired();
            builder.Property(m => m.GroupName).IsRequired();
            builder.HasOne(m => m.ProjectInfo)
                .WithMany(m => m.MaterialGroups)
                .HasForeignKey(m => m.ProjectId)
                .HasPrincipalKey(m => m.Id);
            builder.ToTable("MaterialGroup");
        }
    }
}
