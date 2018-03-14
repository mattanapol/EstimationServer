using Estimation.Domain.Models;
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
        public IEnumerable<SubMaterialDb> SubMaterials { get; set; }

        /// <summary>
        /// Material class
        /// </summary>
        public MaterialClass Class { get; set; }
    }
}
