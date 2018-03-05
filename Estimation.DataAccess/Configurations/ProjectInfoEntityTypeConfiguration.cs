using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class ProjectInfoEntityTypeConfiguration : IEntityTypeConfiguration<ProjectInfoDb>
    {
        public void Configure(EntityTypeBuilder<ProjectInfoDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Code).IsRequired();
            builder.Property(m => m.LabourCost).IsRequired();
            builder.Property(m => m.MiscellaneousIsUsePercentage).IsRequired();
            builder.Property(m => m.TransportationIsUsePercentage).IsRequired();
            builder.ToTable("ProjectInfo");
        }
    }
}
