using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Estimation.Common.AutoMapper.Profiles;

namespace Estimation.Common.AutoMapper
{
    public sealed class AutoMapperConfig
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AutoMapperConfig"/>
        /// </summary>
        private AutoMapperConfig()
        {

        }

        /// <summary>
        /// Registers all mapping globally
        /// </summary>
        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConfigurationProfile>();
                cfg.AddProfile<MainMaterialProfile>();
                cfg.AddProfile<MaterialGroupProfile>();
                cfg.AddProfile<MaterialProfile>();
                cfg.AddProfile<ProjectInfoProfile>();
                cfg.AddProfile<ProjectMaterialProfile>();
                cfg.AddProfile<ProjectSummaryProfile>();
                cfg.AddProfile<ProjectMaterialProfile>();
            });
            //config.AssertConfigurationIsValid();
            IMapper mapper = config.CreateMapper();
            AutoMapperProvider.SetMapper(mapper);
        }
    }
}
