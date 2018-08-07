using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectScopeOfWork: IPrintable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is include.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is include; otherwise, <c>false</c>.
        /// </value>
        public bool IsInclude { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        public Dictionary<string, string> GetDataDictionary()
        {
            var dataDict = new Dictionary<string, string>
            {
                {
                    "Description", Description
                },
                {
                    "IsInclude", IsInclude.ToString()
                },
                {
                    "Remarks", Remarks
                },
                {
                    "Order", (Order + 1).ToString()
                }
            };

            return dataDict;
        }

        public string TargetClass => "scope-of-work";
        public IEnumerable<IPrintable> Child => null;
    }
}
