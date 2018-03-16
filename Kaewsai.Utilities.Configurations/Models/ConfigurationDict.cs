using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.Configurations.Models
{
    public class ConfigurationDict : Dictionary<string, ConfigurationEntry>
    {
        private List<ConfigurationEntry> _configurationEntries;

        /// <summary>
        /// Title of configuration set
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of configuration set
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Configuration entries
        /// </summary>
        public List<ConfigurationEntry> ConfigurationEntries
        {
            get
            {
                return _configurationEntries;
            }
            set
            {
                this.Clear();
                foreach (var config in value)
                {
                    this.Add(config.Label, config);
                }
                _configurationEntries = value;
            }
        }
    }
}
