using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Estimation.Domain
{
    public static class DictionaryExtensions
    {
        public static Dictionary<T,V> Combine<T,V>(this Dictionary<T, V> baseDictionary, Dictionary<T, V> dictionary)
        {
            var result = baseDictionary.Concat(dictionary).GroupBy(d => d.Key)
                .ToDictionary(d => d.Key, d => d.First().Value);
            return result;
        }
    }
}

