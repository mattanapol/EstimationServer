using System;
using System.Collections.Generic;
using System.Text;

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
        public IList<MaterialInfo> SubMaterials { get; set; }
    }
}
