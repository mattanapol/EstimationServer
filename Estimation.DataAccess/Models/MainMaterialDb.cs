using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    /// <summary>
    /// Main material model
    /// </summary>
    public class MainMaterialDb : MaterialInfoDb
    {
        /// <summary>
        /// Submaterial
        /// </summary>
        public IEnumerable<MaterialDb> SubMaterials { get; set; }
    }
}
