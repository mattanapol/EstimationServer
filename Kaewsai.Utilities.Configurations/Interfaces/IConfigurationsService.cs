using Kaewsai.Utilities.Configurations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.Utilities.Configurations.Interfaces
{
    public interface IConfigurationsService
    {
        Task<ConfigurationDict> GetConfigurationByTitle(string title);

        Task<IEnumerable<string>> GetAllConfigurationTitle();

        Task<ConfigurationDict> CreateConfiguration(ConfigurationDict configurationDict);

        Task<ConfigurationDict> UpdateConfiguration(string title, ConfigurationDict configurationDict);

        Task<ConfigurationDict> GetConfigurationByTitleOrDefault(string title, ConfigurationDict defaultConfig = null);

        Task<IEnumerable<ConfigurationDict>> GetAllConfiguration();

        Task CreateDefaultConfigurations();

        Task MigrateAsync();
    }
}
