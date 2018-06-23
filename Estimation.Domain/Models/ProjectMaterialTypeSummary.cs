using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectMaterialTypeSummary: GroupSummary, IPrintable
    {
        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public string MaterialType { get; set; }

        /// <inheritdoc cref="IPrintable" />
        public override Dictionary<string, string> GetDataDictionary()
        {
            var baseDataDict = base.GetDataDictionary();
            var dataDict = new Dictionary<string, string>
            {
                {
                    "MaterialType", MaterialType
                }
            };
            dataDict = dataDict.Combine(baseDataDict);
            return dataDict;
        }

        /// <inheritdoc />
        public override string TargetClass => "material-type-group-summary";

        /// <inheritdoc />
        public override IEnumerable<IPrintable> Child => ChildSummaries;
    }
}
