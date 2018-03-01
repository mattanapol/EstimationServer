using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class MaterialDb : BaseMaterialDb
    {
        /// <summary>
        /// Parent material(Main material of this submaterial)
        /// </summary>
        public SubMaterialDb SubMaterial { get; set; }

        /// <summary>
        /// ForeignKey for main material
        /// </summary>
        public int SubMaterialId { get; set; }
    }
}
