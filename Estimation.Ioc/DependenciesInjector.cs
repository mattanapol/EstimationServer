using Estimation.Common.AutoMapper;
using Estimation.DataAccess;
using Estimation.Interface;
using Kaewsai.Utilities.Configurations;
using Kaewsai.Utilities.Configurations.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Ioc
{
    public class DependenciesInjector
    {
        IServiceCollection _services;
        IConfiguration _configuration;

        public DependenciesInjector(IServiceCollection services, IConfiguration configuration)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Inject()
        {
            _services.AddSingleton<Interface.ITypeMappingService, AutoMapperService>();

            _services.AddScoped<MaterialDbContext>((arg) => {
                return new MaterialDbContext(_configuration.GetConnectionString("MaterialDb"));
            });

            _services.AddScoped<ProjectDbContext>((arg) => {
                return new ProjectDbContext(_configuration.GetConnectionString("ProjectDb"));
            });
            
            _services.AddScoped<ConfigurationDbContext>((arg) => {
                return new ConfigurationDbContext(_configuration.GetConnectionString("ConfigurationDb"));
            });

            _services.AddScoped<IAppDbMigrationService, AppDbMigrationService>();

            RepositoriesInjector.Inject(_services);
            ServicesInjector.Inject(_services);
        }
    }
}
