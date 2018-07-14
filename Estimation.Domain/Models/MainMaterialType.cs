using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    public class MainMaterialType : IPrintable
    {
        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public string MaterialType { get; set; }
        /// <summary>
        /// Gets or sets the main materials.
        /// </summary>
        /// <value>
        /// The main materials.
        /// </value>
        public IList<MainMaterial> MainMaterials { get; set; }

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
        public string TargetClass => "material-type-group";

        /// <inheritdoc />
        public IEnumerable<IPrintable> Child => MainMaterials;
    }
}
