using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class ProjectScopeOfWorkGroupEntityTypeConfiguration : IEntityTypeConfiguration<ProjectScopeOfWorkGroupDb>
    {
        public void Configure(EntityTypeBuilder<ProjectScopeOfWorkGroupDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.MaterialType).IsRequired();
            builder.Property(m => m.ProjectId).IsRequired();
            builder.HasOne(m => m.ProjectInfo)
                .WithMany(m => m.ScopeOfWorkGroups)
                .HasForeignKey(m => m.ProjectId)
                .HasPrincipalKey(m => m.Id);
            builder.ToTable("ScopeOfWorkGroups");
        }
    }
}
