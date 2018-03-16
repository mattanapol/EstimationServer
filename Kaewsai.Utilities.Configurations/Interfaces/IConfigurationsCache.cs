using Kaewsai.Utilities.Configurations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.Utilities.Configurations.Interfaces
{
    public interface IConfigurationsCache : IDictionary<string, ConfigurationDict>, IDisposable
    {
        Task<ConfigurationDict> GetValue(string key);

        void SetValue(string key, ConfigurationDict value);
    }
}
