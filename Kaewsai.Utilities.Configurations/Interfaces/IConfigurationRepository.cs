using Kaewsai.Utilities.Configurations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.Utilities.Configurations.Interfaces
{
    public interface IConfigurationRepository
    {
        /// <summary>
        /// Gets the configuration by title async.
        /// </summary>
        /// <returns>The configuration by title async.</returns>
        /// <param name="title">Title.</param>
        Task<ConfigurationDict> GetConfigurationByTitleAsync(string title);

        /// <summary>
        /// Creates the configuration dict async.
        /// </summary>
        /// <returns>The configuration dict async.</returns>
        /// <param name="configurationDict">Configuration dict.</param>
        Task<ConfigurationDict> CreateConfigurationDictAsync(ConfigurationDict configurationDict);

        /// <summary>
        /// Updates the configuration dict async.
        /// </summary>
        /// <returns>The configuration dict async.</returns>
        /// <param name="configurationDict">Configuration dict.</param>
        Task<ConfigurationDict> UpdateConfigurationDictAsync(string title, ConfigurationDict configurationDict);

        /// <summary>
        /// Deletes the configuration dict by title async.
        /// </summary>
        /// <returns>The configuration dict by title async.</returns>
        /// <param name="title">Title.</param>
        Task DeleteConfigurationDictByTitleAsync(string title);

        /// <summary>
        /// Gets all configuration title.
        /// </summary>
        /// <returns>The all configuration title.</returns>
        Task<IEnumerable<string>> GetAllConfigurationTitle();

        /// <summary>
        /// Gets all configuration.
        /// </summary>
        /// <returns>The all configuration.</returns>
        Task<IEnumerable<ConfigurationDict>> GetAllConfiguration();
    }
}
