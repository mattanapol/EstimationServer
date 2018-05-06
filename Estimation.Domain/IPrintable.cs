using System.Collections.Generic;

namespace Estimation.Domain
{
    public interface IPrintable
    {
        /// <summary>
        /// Get data dictionary to parse to string
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetDataDictionary();
    }
}
