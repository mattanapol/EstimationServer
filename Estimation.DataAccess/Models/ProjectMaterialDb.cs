using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class ProjectMaterialDb: BaseMaterialDb
    {
        /// <summary>
        /// Quantity of material
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Material code as string
        /// </summary>
        public string CodeAsString { get; set; }

        /// <summary>
        /// Material group id
        /// </summary>
        public int MaterialGroupId { get; set; }

        /// <summary>
        /// Material group
        /// </summary>
        public MaterialGroupDb MaterialGroup { get; set; }

        /// <summary>
        /// Material class
        /// </summary>
        public MaterialClass Class { get; set; }
    }
}
