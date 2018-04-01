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

        /// <summary>
        /// Total offer price
        /// </summary>
        public decimal TotalOfferPrice { get; set; }

        /// <summary>
        /// Total accessory cost
        /// </summary>
        public decimal TotalAccessory { get; set; }

        /// <summary>
        /// Total fitting cost
        /// </summary>
        public decimal Totalfitting { get; set; }

        /// <summary>
        /// Total support cost
        /// </summary>
        public decimal TotalSupport { get; set; }

        /// <summary>
        /// Total painting cost
        /// </summary>
        public decimal TotalPainting { get; set; }
    }
}
