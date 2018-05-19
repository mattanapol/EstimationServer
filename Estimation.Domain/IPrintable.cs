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

        /// <summary>
        /// Gets the target class.
        /// </summary>
        /// <value>
        /// The target class.
        /// </value>
        string TargetClass { get; }

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        IEnumerable<IPrintable> Child { get; }
    }
}
