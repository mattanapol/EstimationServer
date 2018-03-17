using Estimation.Services.DefaultConfigurations;
using Kaewsai.Utilities.Configurations;
using Kaewsai.Utilities.Configurations.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class AppConfigurationService: ConfigurationsService, IConfigurationsService
    {
        public AppConfigurationService(IConfigurationsCache configurationsCache, IConfigurationRepository configurationDictRepository): base(configurationsCache, configurationDictRepository)
        {
        }

        public override async Task CreateDefaultConfigurations()
        {
            await CreateConfiguration(new MaterialTypeConfigurations());
        }
    }
}
