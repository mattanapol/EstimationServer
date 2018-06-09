using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Services.Logger
{
    public class AppLogger
    {
        /// <summary>
        /// Gets or sets the logger factory.
        /// </summary>
        /// <value>
        /// The logger factory.
        /// </value>
        public static ILoggerFactory LoggerFactory { get; set; } = null;
    }
}
