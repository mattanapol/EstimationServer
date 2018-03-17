using Estimation.Interface;
using Kaewsai.Utilities.Configurations;
using Kaewsai.Utilities.Configurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess
{
    public class AppDbMigrationService : IAppDbMigrationService
    {
        private readonly MaterialDbContext _materialDbContext;
        private readonly ProjectDbContext _projectDbContext;
        private readonly ConfigurationDbContext _configurationsDbContext;
        private readonly IConfigurationsService _configurationsService;

        public AppDbMigrationService(MaterialDbContext materialDbContext, ProjectDbContext projectDbContext, ConfigurationDbContext configurationsDbContext, IConfigurationsService configurationsService)
        {
            _materialDbContext = materialDbContext ?? throw new ArgumentNullException(nameof(materialDbContext));
            _projectDbContext = projectDbContext ?? throw new ArgumentNullException(nameof(projectDbContext));
            _configurationsDbContext = configurationsDbContext ?? throw new ArgumentNullException(nameof(configurationsDbContext));
            _configurationsService = configurationsService ?? throw new ArgumentNullException(nameof(configurationsService));
        }

        public Task DeleteExistingDatabase()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDatabaseExist()
        {
            throw new NotImplementedException();
        }

        public async Task Migrate()
        {
            try
            {
                await _materialDbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Material database migration failed.", ex);
            }
            
            try
            {
                await _projectDbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Project database migration failed.", ex);
            }

            try
            {
                await _configurationsDbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Configuration database migration failed.", ex);
            }
        }

        public async Task Seed()
        {
            await _configurationsService.CreateDefaultConfigurations();
        }
    }
}
