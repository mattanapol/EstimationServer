using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Services.Interfaces
{
    /// <summary>
    /// Type mapping service interface
    /// </summary>
    public interface ITypeMappingService
    {
        /// <summary>
        /// Map object type
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source) where TSource : class where TDestination : class;
    }
}
