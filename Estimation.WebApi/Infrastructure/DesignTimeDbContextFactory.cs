using Estimation.DataAccess;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estimation.WebApi.Infrastructure
{
    /// <summary>
    /// Design time database context
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Design time database context factory
        /// </summary>
        /// <param name="configuration"></param>
        public DesignTimeDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Creates the db context.
        /// </summary>
        /// <returns>The db context.</returns>
        /// <param name="args">Arguments.</param>
        public AppDbContext CreateDbContext(string[] args)
        {
            return new AppDbContext(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
