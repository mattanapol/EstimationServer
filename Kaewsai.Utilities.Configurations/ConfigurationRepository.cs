using Estimation.Interface;
using Kaewsai.Utilities.Configurations.Interfaces;
using Kaewsai.Utilities.Configurations.Models;
using Kaewsai.Utilities.Configurations.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.Utilities.Configurations
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        /// <summary>
        /// The database context with an open connection.
        /// </summary>
        protected ConfigurationDbContext DbContext { get; }

        /// <summary>
        /// Gets the type mapping service.
        /// </summary>
        /// <value>The type mapping service.</value>
        protected ITypeMappingService TypeMappingService { get; }

        public ConfigurationRepository(ConfigurationDbContext dbContext, ITypeMappingService typeMappingService)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            TypeMappingService = typeMappingService ?? throw new ArgumentNullException(nameof(typeMappingService));
        }

        public async Task<ConfigurationDict> CreateConfigurationDictAsync(ConfigurationDict configurationDict)
        {
            var configurationDictDb = TypeMappingService.Map<ConfigurationDict, ConfigurationDictDb>(configurationDict);

            DbContext.ConfigurationDict.Add(configurationDictDb);
            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ConfigurationDictDb, ConfigurationDict>(configurationDictDb);
        }

        public async Task DeleteConfigurationDictByTitleAsync(string title)
        {
            var configurationDictDb = await DbContext.ConfigurationDict
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Title == title);
            DbContext.ConfigurationDict.Remove(configurationDictDb);

            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConfigurationDict>> GetAllConfiguration()
        {
            var configurationDictDb = DbContext.ConfigurationDict
                                               .Include(c => c.ConfigurationEntries)
                                               .Select(c => TypeMappingService.Map<ConfigurationDictDb, ConfigurationDict>(c))
                                               .AsNoTracking();
            var paginatedConfigurationDic = await configurationDictDb.ToListAsync();
            return paginatedConfigurationDic;
        }

        public async Task<IEnumerable<string>> GetAllConfigurationTitle()
        {
            var configurationDictDb = DbContext.ConfigurationDict
                                               .Select(c => c.Title)
                                               .AsNoTracking();
            var paginatedConfigurationDic = await configurationDictDb.ToListAsync();
            return paginatedConfigurationDic;
        }

        public async Task<ConfigurationDict> GetConfigurationByTitleAsync(string title)
        {
            var configurationDictDb = await DbContext.ConfigurationDict
                                                     .Include(c => c.ConfigurationEntries)
                                                     .AsNoTracking()
                                                     .SingleOrDefaultAsync(s => string.Compare(s.Title, title, StringComparison.OrdinalIgnoreCase) == 0);

            return TypeMappingService.Map<ConfigurationDictDb, ConfigurationDict>(configurationDictDb);
        }

        public async Task<ConfigurationDict> UpdateConfigurationDictAsync(string title, ConfigurationDict configurationDict)
        {
            var configurationDictDb = await DbContext.ConfigurationDict
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Title == title);
            if (configurationDictDb != null)
            {
                try
                {
                    DbContext.Entry(configurationDictDb).State = EntityState.Deleted;
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            configurationDictDb = TypeMappingService
                .Map<ConfigurationDict, ConfigurationDictDb>(configurationDict);
            DbContext.ConfigurationDict.Add(configurationDictDb);
            await DbContext.SaveChangesAsync();

            return TypeMappingService.Map<ConfigurationDictDb, ConfigurationDict>(configurationDictDb);
        }
    }
}
