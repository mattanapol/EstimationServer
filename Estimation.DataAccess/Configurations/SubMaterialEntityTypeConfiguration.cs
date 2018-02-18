using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class SubMaterialEntityTypeConfiguration : IEntityTypeConfiguration<SubMaterialDb>
    {
        public void Configure(EntityTypeBuilder<SubMaterialDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.MainMaterialId).IsRequired();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Code).IsRequired();
            builder.HasOne(m => m.MainMaterial)
                .WithMany(m => m.SubMaterials)
                .HasForeignKey(m => m.MainMaterialId)
                .HasPrincipalKey(m => m.Id);
            builder.ToTable("SubMaterials");
        }
    }
}
