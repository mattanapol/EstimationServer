using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectScopeOfWorkGroup: IPrintable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public string MaterialType { get; set; }

        /// <summary>
        /// Gets or sets the scope of works.
        /// </summary>
        /// <value>
        /// The scope of works.
        /// </value>
        public IEnumerable<ProjectScopeOfWork> ScopeOfWorks { get; set; }

        public Dictionary<string, string> GetDataDictionary()
        {
            var dataDict = new Dictionary<string, string>
            {
                {
                    "MaterialType", MaterialType
                }
            };

            return dataDict;
        }

        public string TargetClass => "scope-of-work-group";
        public IEnumerable<IPrintable> Child => ScopeOfWorks;
    }
}
