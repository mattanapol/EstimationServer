using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Estimation.Ioc;
using Estimation.Services.Logger;
using Estimation.WebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Estimation.WebApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentPath = env.ContentRootPath;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Get current path
        /// </summary>
        public string CurrentPath { get; }

        /// <summary>
        /// Config services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { })
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.Converters.Add(new KeyValuePairConverter());
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy
                            {
                                ProcessDictionaryKeys = true
                            }
                        };
                        options.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.Formatting = Formatting.Indented;
                        // Set json converter in other places to be like this.
                        JsonConvert.DefaultSettings = () => options.SerializerSettings;
                    });

            // Cors
            services.AddCors();

            // Dependencies Injection
            DependenciesInjector dependenciesInjector = new DependenciesInjector(services, Configuration);
            dependenciesInjector.Inject();

#if DEBUG || CLOUD
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Estimation WebApi", Version = "V0.1" });
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments(Path.Combine(CurrentPath, @"Estimation.WebApi.xml"));
            });
#endif
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            ConfigureLog(loggerFactory);
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
#if DEBUG || ADMIN || CLOUD
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
#endif

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            EnableCors(app);
            app.UseMvc();
        }

        /// <summary>
        /// Enables cors policies.
        /// </summary>
        /// <param name="app">App.</param>
        private static void EnableCors(IApplicationBuilder app)
        {
            app.UseCors(builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod());
        }

        /// <summary>
        /// Configure log
        /// </summary>
        /// <param name="loggerFactory"></param>
        private void ConfigureLog(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile(Path.Combine(CurrentPath, @"Logs/ts-{Date}.txt"));
            AppLogger.LoggerFactory = loggerFactory;
        }

        //private void DependenciesInject(IServiceCollection services)
        //{
        //    // Data Layer
        //    // Set connection string
        //    services.AddScoped<MaterialDbContext>((arg) => {
        //        return new MaterialDbContext(Configuration.GetConnectionString("MaterialDb"));
        //    });
        //}
    }
}
