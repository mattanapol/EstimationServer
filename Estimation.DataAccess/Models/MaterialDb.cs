﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class MaterialDb : MaterialInfoDb
    {
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
        /// Parent material(Main material of this submaterial)
        /// </summary>
        public MainMaterialDb MainMaterial { get; set; }

        /// <summary>
        /// ForeignKey for main material
        /// </summary>
        public int MainMaterialId { get; set; }
    }
}
