using Estimation.DataAccess.Repositories;
using Estimation.Services.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Ioc
{
    internal class RepositoriesInjector
    {
        internal static void Inject(IServiceCollection services)
        {
            InjectMaterialRepository(services);
            InjectProjectRepository(services);
        }

        private static void InjectMaterialRepository(IServiceCollection services)
        {
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ISubMaterialRepository, SubMaterialRepository>();
            services.AddScoped<IMainMaterialRepository, MainMaterialRepository>();
        }

        private static void InjectProjectRepository(IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectMaterialGroupRepository, ProjectMaterialGroupRepository>();
        }
    }
}
