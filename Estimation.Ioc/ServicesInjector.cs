using Estimation.Services;
using Estimation.Interface;
using Kaewsai.Utilities.Configurations;
using Kaewsai.Utilities.Configurations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Kaewsai.HtmlToPdf.Interface;
using Kaewsai.HtmlToPdf;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace Estimation.Ioc
{
    internal class ServicesInjector
    {
        internal static void Inject(IServiceCollection services)
        {
            InjectProjectServices(services);
            InjectConfigurationServices(services);
        }

        private static void InjectProjectServices(IServiceCollection services)
        {
            services.AddScoped<IProjectMaterialGroupService, ProjectMaterialGroupService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectSummaryService, ProjectSummaryService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IExportService, ExportService>();
            // Html to pdf
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
        }

        private static void InjectConfigurationServices(IServiceCollection services)
        {
            services.AddScoped<IConfigurationsCache, ConfigurationsCache>();
            services.AddScoped<IConfigurationsService, AppConfigurationService>();
        }
    }
}
