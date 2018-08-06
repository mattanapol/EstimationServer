using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Configurations
{
    internal class ProjectScopeOfWorkEntityTypeConfiguration : IEntityTypeConfiguration<ProjectScopeOfWorkDb>
    {
        public void Configure(EntityTypeBuilder<ProjectScopeOfWorkDb> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.IsInclude).IsRequired();
            builder.HasOne(m => m.ScopeOfWorkGroup)
                .WithMany(m => m.ProjectScopeOfWorks)
                .HasForeignKey(m => m.ScopeOfWorkGroupId)
                .HasPrincipalKey(m => m.Id);
            builder.ToTable("ScopeOfWorks");
        }
    }
}
