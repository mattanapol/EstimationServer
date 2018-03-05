using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services.Interfaces
{
    public interface IAppDbMigrationService
    {
        Task Migrate();

        Task Seed();

        Task<bool> IsDatabaseExist();

        Task DeleteExistingDatabase();
    }
}
