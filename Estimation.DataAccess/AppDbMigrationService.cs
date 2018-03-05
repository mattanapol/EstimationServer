using Estimation.Services.Interfaces;
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

        public AppDbMigrationService(MaterialDbContext materialDbContext, ProjectDbContext projectDbContext)
        {
            _materialDbContext = materialDbContext ?? throw new ArgumentNullException(nameof(materialDbContext));
            _projectDbContext = projectDbContext ?? throw new ArgumentNullException(nameof(projectDbContext));
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
        }

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}
