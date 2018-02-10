using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Estimation.WebApi
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <returns>The web host.</returns>
        /// <param name="args">Arguments.</param>
        public static IWebHost BuildWebHost(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                   .UseKestrel()
#if DEBUG || CLOUD
                   .UseEnvironment("Development")
#endif
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseUrls("http://*:8989")
                   .UseStartup<Startup>()
                   .Build();

            return host;
        }
    }
}
