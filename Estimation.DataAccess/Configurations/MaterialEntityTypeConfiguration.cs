using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class MaterialEntityTypeConfiguration : IEntityTypeConfiguration<MaterialDb>
    {
        public void Configure(EntityTypeBuilder<MaterialDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.SubMaterialId).IsRequired();
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Code).IsRequired();
            builder.HasOne(m => m.SubMaterial)
                .WithMany(m => m.Materials)
                .HasForeignKey(m => m.SubMaterialId)
                .HasPrincipalKey(m => m.Id);
            builder.ToTable("Materials");
        }
    }
}
