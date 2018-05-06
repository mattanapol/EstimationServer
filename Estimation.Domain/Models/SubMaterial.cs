using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    public class SubMaterial: MaterialInfo
    {
        /// <summary>
        /// Submaterial
        /// </summary>
        public IEnumerable<Material> Materials { get; set; }
    }
}
