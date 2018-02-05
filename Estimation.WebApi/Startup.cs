using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Services.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Estimation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentPath = env.ContentRootPath;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UK");
        }

        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Get current path
        /// </summary>
        public string CurrentPath { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
            })
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                        options.SerializerSettings.StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeNonAscii;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy
                            {
                                ProcessDictionaryKeys = true
                            }
                        };
                        options.SerializerSettings.FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Decimal;
                    });

            // Cors
            services.AddCors();

            // Dependencies Injection
            DependenciesInject(services);
#if DEBUG || CLOUD
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Estimation WebApi", Version = "V0.1" });
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments(Path.Combine(CurrentPath, @"MiniWing.WebApi.xml"));
            });
#endif
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
    }
}
