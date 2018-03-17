using Kaewsai.Utilities.Configurations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Estimation.WebApi.Infrastructure
{
    public class DesignTimeConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        /// <summary>
        /// Design time database context factory
        /// </summary>
        public DesignTimeConfigurationDbContextFactory()
        {
        }

        /// <summary>
        /// Creates the db context.
        /// </summary>
        /// <returns>The db context.</returns>
        /// <param name="args">Arguments.</param>
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            return new ConfigurationDbContext(configuration.GetConnectionString("ConfigurationDb"));
        }
    }
}
