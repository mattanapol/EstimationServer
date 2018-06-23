using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectMaterialType: IPrintable
    {
        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public string MaterialType { get; set; }
        /// <summary>
        /// Gets or sets the project material groups.
        /// </summary>
        /// <value>
        /// The project material groups.
        /// </value>
        public IList<ProjectMaterialGroup> ProjectMaterialGroups { get; set; }

        /// <inheritdoc cref="IPrintable" />
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

        /// <inheritdoc />
        public string TargetClass => "material-type-group-summary";

        /// <inheritdoc />
        public IEnumerable<IPrintable> Child => ProjectMaterialGroups;
    }
}
