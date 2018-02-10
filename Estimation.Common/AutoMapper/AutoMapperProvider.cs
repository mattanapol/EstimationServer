using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Common.AutoMapper
{
    public static class AutoMapperProvider
    {
        /// <summary>
        /// The variable is declared to be volatile to ensure that assignment to the instance variable completes before the instance variable can be accessed.
        /// </summary>
        private static volatile IMapper _instance;
        private static object _syncRoot = new object();


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static IMapper Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Sets the AutoMapper instance.
        /// </summary>
        /// <param name="mapper">The configured <see cref="Mapper"/> instance.</param>
        public static void SetMapper(IMapper mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            lock (_syncRoot)
            {
                _instance = mapper;
            }
        }
    }
}
