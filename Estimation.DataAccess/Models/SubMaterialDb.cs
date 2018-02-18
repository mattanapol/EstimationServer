using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    /// <summary>
    /// Sub Material
    /// </summary>
    public class SubMaterialDb: MaterialInfoDb
    {
        /// <summary>
        /// Material
        /// </summary>
        public IEnumerable<MaterialDb> Materials { get; set; }

        /// <summary>
        /// Parent material(Main material of this submaterial)
        /// </summary>
        public MainMaterialDb MainMaterial { get; set; }

        /// <summary>
        /// ForeignKey for main material
        /// </summary>
        public int MainMaterialId { get; set; }
    }
}
