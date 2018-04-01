using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectMaterial: Material
    {
        /// <summary>
        /// Labour cost (Manday x Labour)
        /// </summary>
        public decimal LabourCost { get; set; }

        /// <summary>
        /// Installation cost (LabourCost x Quantity)
        /// </summary>
        public decimal Installation { get; set; }

        /// <summary>
        /// Quantity of material
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Total cost of this material
        /// </summary>
        public decimal TotalCost { get; set; }
    }
}
