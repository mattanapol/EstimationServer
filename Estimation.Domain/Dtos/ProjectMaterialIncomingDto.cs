using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class ProjectMaterialIncomingDto
    {
        /// <summary>
        /// Material Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Material class
        /// </summary>
        [Required]
        public MaterialClass Class { get; set; }

        /// <summary>
        /// Quantity of material
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public string CodeAsString { get; set; }

        /// <summary>
        /// List Price
        /// </summary>
        public decimal ListPrice { get; set; }

        /// <summary>
        /// Net Price
        /// </summary>
        public decimal NetPrice { get; set; }

        /// <summary>
        /// Offer Price
        /// </summary>
        public decimal OfferPrice { get; set; }

        /// <summary>
        /// Manpower (Man x Day)
        /// </summary>
        public decimal Manpower { get; set; }

        /// <summary>
        /// Fittings
        /// </summary>
        public decimal Fittings { get; set; }

        /// <summary>
        /// Accessory
        /// </summary>
        public decimal Accessory { get; set; }

        /// <summary>
        /// Supporting
        /// </summary>
        public decimal Supporting { get; set; }

        /// <summary>
        /// Painting
        /// </summary>
        public decimal Painting { get; set; }

        /// <summary>
        /// Remarks
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Unit of material
        /// </summary>
        public string Unit { get; set; }
    }
}
