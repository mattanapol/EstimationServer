using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Estimation.DataAccess
{
    class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MiniWing.DataAccess.MiniWingDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public AppDbContext(string connectionString) : base(CreateSqliteOptions(connectionString))
        {
        }

        /// <summary>
        /// Check if database already exist.
        /// </summary>
        /// <returns></returns>
        public bool IsDatabaseExist()
        {
            return (this.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }

        public string GetDatabasePath()
        {
            return this.GetDatabasePath();
        }

        /// <summary>
        /// On the model creating.
        /// </summary>
        /// <param name="modelBuilder">Builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new InvoiceRunningNumberConfiguration());
            //modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            //modelBuilder.ApplyConfiguration(new OldGoldSupplierConfiguration());
        }

        /// <summary>
        /// Creates the sqlite provider options.
        /// </summary>
        /// <returns>The sqlite options.</returns>
        /// <param name="connectionString">Connection string.</param>
        private static DbContextOptions CreateSqliteOptions(string connectionString)
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder();
            dbContextOptionBuilder.UseSqlite(connectionString);
            dbContextOptionBuilder.EnableSensitiveDataLogging();
            return dbContextOptionBuilder.Options;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The changes.</returns>
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <returns>The changes async.</returns>
        /// <param name="cancellationToken">Cancellation token.</param>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Adds created date and last modified date timestamps to entities.
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                }

                if (entity.State == EntityState.Modified)
                {
                    Entry((BaseEntity)entity.Entity).Property(e => e.CreatedDate).IsModified = false;
                }

                ((BaseEntity)entity.Entity).LastModifiedDate = DateTime.UtcNow;
            }
        }
    }
}
