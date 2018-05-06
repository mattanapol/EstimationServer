using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Main material model
    /// </summary>
    public class MainMaterial: MaterialInfo
    {
        /// <summary>
        /// Submaterial
        /// </summary>
        public IEnumerable<SubMaterial> SubMaterials { get; set; }
    }
}
