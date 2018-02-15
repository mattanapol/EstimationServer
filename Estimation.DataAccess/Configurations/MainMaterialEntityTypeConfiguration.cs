using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class MainMaterialEntityTypeConfiguration : IEntityTypeConfiguration<MainMaterialDb>
    {
        public void Configure(EntityTypeBuilder<MainMaterialDb> builder)
        {
            builder.HasKey(t => new { t.Id });
            builder.ToTable("MainMaterials");
        }
    }
}
