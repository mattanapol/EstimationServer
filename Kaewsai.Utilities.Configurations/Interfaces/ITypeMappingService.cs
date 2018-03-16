using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.Configurations.Interfaces
{
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
