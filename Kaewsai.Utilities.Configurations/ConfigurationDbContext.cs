using Kaewsai.Utilities.Configurations.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.Configurations
{
    /// <summary>
    /// Configuration db context
    /// </summary>
    /// <remarks>
    /// dotnet ef migrations remove -c ConfigurationDbContext -s ..\Estimation.WebApi
    /// dotnet ef migrations add InitialConfigurationDb -c ConfigurationDbContext -s ..\Estimation.WebApi
    /// </remarks>
    public class ConfigurationDbContext: DbContext
    {
        /// <summary>
        /// Gets or sets the configuration dict.
        /// </summary>
        /// <value>The configuration dict.</value>
        public DbSet<ConfigurationDictDb> ConfigurationDict { get; set; }

        /// <summary>
        /// Gets or sets the configuration entries.
        /// </summary>
        /// <value>The configuration entries.</value>
        public DbSet<ConfigurationEntriesDb> ConfigurationEntries { get; set; }

        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MiniWing.DataAccess.MiniWingDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public ConfigurationDbContext(string connectionString) : base()
        {
            _connectionString = Environment.ExpandEnvironmentVariables(connectionString);
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
