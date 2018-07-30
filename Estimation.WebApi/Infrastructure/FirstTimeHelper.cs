using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Estimation.WebApi.Infrastructure
{
    /// <summary>
    /// First time helper class
    /// </summary>
    public class FirstTimeHelper
    {
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Gets the template configuration database path.
        /// </summary>
        /// <value>
        /// The template configuration database path.
        /// </value>
        public string TemplateConfigurationDbPath => "./ProgramData/Configurations.db";
        /// <summary>
        /// Gets the template material database path.
        /// </summary>
        /// <value>
        /// The template material database path.
        /// </value>
        public string TemplateMaterialDbPath => "./ProgramData/Materials.db";
        /// <summary>
        /// Gets the template project database path.
        /// </summary>
        /// <value>
        /// The template project database path.
        /// </value>
        public string TemplateProjectDbPath => "./ProgramData/Projects.db";

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstTimeHelper"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public FirstTimeHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Firsts the time configuration.
        /// </summary>
        public void FirstTimeConfig()
        {
            string pathToConfigurationDb = Environment.ExpandEnvironmentVariables(_configuration.GetConnectionString("ConfigurationDb").Split('=')[1]);
            string pathToMaterialDb = Environment.ExpandEnvironmentVariables(_configuration.GetConnectionString("MaterialDb").Split('=')[1]);
            string pathToProjectDb = Environment.ExpandEnvironmentVariables(_configuration.GetConnectionString("ProjectDb").Split('=')[1]);

            if (!File.Exists(pathToConfigurationDb))
            {
                Console.WriteLine($"Copy Configuration database {TemplateConfigurationDbPath} to {pathToConfigurationDb}");
                Directory.CreateDirectory(Path.GetDirectoryName(pathToConfigurationDb));
                File.Copy(TemplateConfigurationDbPath, pathToConfigurationDb);
            }

            if (!File.Exists(pathToMaterialDb))
            {
                Console.WriteLine($"Copy Configuration database {TemplateMaterialDbPath} to {pathToMaterialDb}");
                Directory.CreateDirectory(Path.GetDirectoryName(pathToMaterialDb));
                File.Copy(TemplateMaterialDbPath, pathToMaterialDb);
            }

            if (!File.Exists(pathToProjectDb))
            {
                Console.WriteLine($"Copy Configuration database {TemplateProjectDbPath} to {pathToProjectDb}");
                Directory.CreateDirectory(Path.GetDirectoryName(pathToProjectDb));
                File.Copy(TemplateProjectDbPath, pathToProjectDb);
            }
        }
    }
}
