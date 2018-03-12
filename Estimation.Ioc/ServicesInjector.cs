using Estimation.Services;
using Estimation.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Ioc
{
    internal class ServicesInjector
    {
        internal static void Inject(IServiceCollection services)
        {
            InjectProjectServices(services);
        }

        private static void InjectProjectServices(IServiceCollection services)
        {
            services.AddScoped<IProjectMaterialGroupService, ProjectMaterialGroupService>();
            services.AddScoped<IProjectService, ProjectService>();
        }
    }
}
