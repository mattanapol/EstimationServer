using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Services.Logger
{
    public class AppLogger
    {
        private static ILoggerFactory _Factory = null;

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                return _Factory;
            }
            set { _Factory = value; }
        }
    }
}
