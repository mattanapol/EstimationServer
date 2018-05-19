using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    public class SubMaterial: MaterialInfo, IPrintable
    {
        /// <summary>
        /// Sub material
        /// </summary>
        public IEnumerable<Material> Materials { get; set; }

        /// <inheritdoc />
        public string TargetClass => "sub-material";

        /// <summary>
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        public IEnumerable<IPrintable> Child => Materials;
    }
}
