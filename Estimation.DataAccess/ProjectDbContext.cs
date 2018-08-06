using Estimation.DataAccess.Configurations;
using Estimation.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess
{
    /// <summary>
    /// Project database context
    /// </summary>
    /// <remarks>
    /// dotnet ef migrations remove -c ProjectDbContext -s ..\Estimation.WebApi
    /// dotnet ef migrations add InitialProjectDb -c ProjectDbContext -s ..\Estimation.WebApi
    /// </remarks>
    public class ProjectDbContext: AppDbContext
    {
        
        /// <summary>
        /// Project information database set
        /// </summary>
        public DbSet<ProjectInfoDb> ProjectInfo { get; set; }

        /// <summary>
        /// Material group database set
        /// </summary>
        public DbSet<MaterialGroupDb> MaterialGroup { get; set; }

        /// <summary>
        /// Material database set
        /// </summary>
        public DbSet<ProjectMaterialDb> Material { get; set; }

        /// <summary>
        /// Gets or sets the scope of work.
        /// </summary>
        /// <value>
        /// The scope of work.
        /// </value>
        //public DbSet<ProjectScopeOfWorkDb> ScopeOfWork { get; set; }

        /// <summary>
        /// Gets or sets the scope of work group.
        /// </summary>
        /// <value>
        /// The scope of work group.
        /// </value>
        //public DbSet<ProjectScopeOfWorkGroupDb> ScopeOfWorkGroup { get; set; }

        /// <summary>
        /// Project database context
        /// </summary>
        /// <param name="connectionString"></param>
        public ProjectDbContext(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// On the model creating.
        /// </summary>
        /// <param name="modelBuilder">Builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProjectInfoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialGroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectMaterialEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ProjectScopeOfWorkEntityTypeConfiguration());
            //modelBuilder.ApplyConfiguration(new ProjectScopeOfWorkGroupEntityTypeConfiguration());
        }
    }
}
