using Estimation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.DataAccess
{
    public class MaterialDbMigrationService : IMaterialDbMigrationService
    {
        private readonly MaterialDbContext _materialDbContext;

        public MaterialDbMigrationService(MaterialDbContext materialDbContext)
        {
            _materialDbContext = materialDbContext ?? throw new ArgumentNullException(nameof(materialDbContext));
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
            await _materialDbContext.Database.MigrateAsync();
        }

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}
