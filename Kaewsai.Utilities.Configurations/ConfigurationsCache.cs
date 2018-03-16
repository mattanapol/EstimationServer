using Kaewsai.Utilities.Configurations.Interfaces;
using Kaewsai.Utilities.Configurations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.Utilities.Configurations
{
    public class ConfigurationsCache : Dictionary<string, ConfigurationDict>, IConfigurationsCache, IDisposable
    {
        private readonly IConfigurationRepository _configurationDictRepository;
        private Dictionary<string, bool> _isKeyBeenModified;

        public ConfigurationsCache(IConfigurationRepository configurationDictRepository)
        {
            _configurationDictRepository = configurationDictRepository ?? throw new ArgumentNullException(nameof(configurationDictRepository));
            _isKeyBeenModified = new Dictionary<string, bool>();
        }

        public async Task<ConfigurationDict> GetValue(string key)
        {
            key = key.ToUpper();
            ConfigurationDict config;
            if (!this.TryGetValue(key, out config))
            {
                config = await _configurationDictRepository.GetConfigurationByTitleAsync(key);
                this[key] = config;
                _isKeyBeenModified[key] = false;
            }
            return config;
        }

        public void Dispose()
        {
            int updateCounter = 0;
            foreach (var kp in _isKeyBeenModified)
            {
                if (kp.Value)
                {
                    _configurationDictRepository.UpdateConfigurationDictAsync(kp.Key, this[kp.Key]);
                    updateCounter++;
                }
            }
            Console.WriteLine($"Update configurations of {updateCounter} record(s).");
            _isKeyBeenModified.Clear();
            this.Clear();
        }

        public void SetValue(string key, ConfigurationDict value)
        {
            this[key] = value;
            _isKeyBeenModified[key] = true;
        }
    }
}
