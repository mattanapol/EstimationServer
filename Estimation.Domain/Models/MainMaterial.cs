using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Main material model
    /// </summary>
    public class MainMaterial: MaterialInfo, IPrintable
    {
        /// <summary>
        /// Sub Material
        /// </summary>
        public IEnumerable<SubMaterial> SubMaterials { get; set; }

        /// <inheritdoc />
        public string TargetClass => "main-material";

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<IPrintable> Child => SubMaterials;
    }
}
