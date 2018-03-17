using Kaewsai.Utilities.Configurations.Interfaces;
using Kaewsai.Utilities.Configurations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.Utilities.Configurations
{
    public abstract class ConfigurationsService : IConfigurationsService
    {
        IConfigurationsCache _configurationsCache;
        IConfigurationRepository _configurationDictRepository;

        public ConfigurationsService(IConfigurationsCache configurationsCache, IConfigurationRepository configurationDictRepository)
        {
            _configurationsCache = configurationsCache ?? throw new ArgumentNullException(nameof(configurationsCache));
            _configurationDictRepository = configurationDictRepository ?? throw new ArgumentNullException(nameof(configurationDictRepository));
        }

        public async Task<ConfigurationDict> CreateConfiguration(ConfigurationDict configurationDict)
        {
            await Task.Run(() => _configurationsCache.SetValue(configurationDict.Title, configurationDict));
            return configurationDict;
        }

        public async Task<IEnumerable<string>> GetAllConfigurationTitle()
        {
            return await _configurationDictRepository.GetAllConfigurationTitle();
        }

        public async Task<ConfigurationDict> GetConfigurationByTitle(string title)
        {
            var config = await _configurationsCache.GetValue(title);
            if (config == null)
                throw new NullReferenceException($"There is no configuration with title = {title} in database.");
            return config;
        }

        public async Task<ConfigurationDict> GetConfigurationByTitleOrDefault(string title, ConfigurationDict defaultConfig = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            if (defaultConfig == null)
                defaultConfig = new ConfigurationDict() { Title = title, Description = "Default config." };

            var config = await _configurationsCache.GetValue(title);
            if (config == null || config.Count < defaultConfig.Count)
            {
                config = defaultConfig;
                _configurationsCache.SetValue(title, config);
            }

            return config;
        }

        public async Task<ConfigurationDict> UpdateConfiguration(string title, ConfigurationDict configurationDict)
        {
            await Task.Run(() => _configurationsCache.SetValue(title, configurationDict));
            return configurationDict;
        }

        public async Task<IEnumerable<ConfigurationDict>> GetAllConfiguration()
        {
            return await _configurationDictRepository.GetAllConfiguration();
        }

        public abstract Task CreateDefaultConfigurations();

        public Task MigrateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
