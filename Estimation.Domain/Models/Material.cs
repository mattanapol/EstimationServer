using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class Material: MaterialInfo
    {
        /// <summary>
        /// Material type
        /// </summary>
        public override Type MaterialType
        {
            get => MainMaterial.MaterialType;
        }

        private string _code;

        public override string Code
        {
            get => $"{MainMaterial.Code}-{_code}";
            set => _code = value;
        }

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
        public MaterialInfo MainMaterial { get; set; }
    }
}
