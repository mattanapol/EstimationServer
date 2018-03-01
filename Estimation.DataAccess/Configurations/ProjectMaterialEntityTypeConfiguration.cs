using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class ProjectMaterialEntityTypeConfiguration : IEntityTypeConfiguration<ProjectMaterialDb>
    {
        public void Configure(EntityTypeBuilder<ProjectMaterialDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.MaterialGroupId).IsRequired();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.CodeAsString).IsRequired();
            builder.HasOne(m => m.MaterialGroup)
                .WithMany(m => m.Materials)
                .HasForeignKey(m => m.MaterialGroupId)
                .HasPrincipalKey(m => m.Id);
            builder.ToTable("ProjectMaterials");
        }
    }
}
