using Estimation.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Common.AutoMapper
{
    /// <summary>
    /// Auto mapper service
    /// </summary>
    public class AutoMapperService : ITypeMappingService
    {
        /// <summary>
        /// Auto mapper service
        /// </summary>
        public AutoMapperService()
        {
            if (AutoMapperProvider.Instance == null)
                AutoMapperConfig.RegisterMappings();
        }

        /// <summary>
        /// Map object
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class
        {
            return AutoMapperProvider.Instance.Map<TSource, TDestination>(source);
        }
    }
}
