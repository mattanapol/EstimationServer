using Estimation.DataAccess;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Estimation.WebApi.Infrastructure
{
    /// <summary>
    /// Design time database context
    /// </summary>
    public class DesignTimeMaterialDbContextFactory : IDesignTimeDbContextFactory<MaterialDbContext>
    {
        //private readonly IConfiguration _configuration;

        /// <summary>
        /// Design time database context factory
        /// </summary>
        public DesignTimeMaterialDbContextFactory()
        {
            //_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Creates the db context.
        /// </summary>
        /// <returns>The db context.</returns>
        /// <param name="args">Arguments.</param>
        public MaterialDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            return new MaterialDbContext(configuration.GetConnectionString("MaterialDb"));
        }
    }
}
