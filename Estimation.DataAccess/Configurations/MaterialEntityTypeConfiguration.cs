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
            builder.HasKey(t => new { t.Id });
            builder.ToTable("Materials");
            builder.HasOne(m => m.MainMaterial)
                .WithMany(m => m.SubMaterials)
                .HasForeignKey(m => m.MainMaterialId);
        }
    }
}
