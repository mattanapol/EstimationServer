using System;
using System.Collections.Generic;
using System.Text;
using DbfReader;

namespace MigrationTool
{
    public class Material
    {
        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Material code as string
        /// </summary>
        public string CodeAsString { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

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

        public static Material CreateMaterialFromRow(
            IDbfRow row)
        {
            Material material = new Material()
            {
                CodeAsString = row["CODE"]
                    .GetString().Trim(),
                Name = row["MATERIAL"]
                    .GetString()
                    .Trim(),
                Description = row["DESCRIPT"]
                    .GetString().Trim(),
                Unit = string.IsNullOrWhiteSpace(row["UNIT"].GetString())
                    ? "LOT"
                    : row["UNIT"]
                        .GetString().Trim(),
                ListPrice = row["LISTPRICE"].ForceDecimal(),
                NetPrice = row["NETPRICE"].ForceDecimal(),
                OfferPrice = row["ESTPRICE"].ForceDecimal(),
                Manpower = row["MANPOWER"].ForceDecimal(),
                Accessory = row["ACCESSORY"].ForceDecimal(),
                Supporting = row["SUPPORT"].ForceDecimal(),
                Painting = row["PAINTING"].ForceDecimal(),
                Remark = row["REMARKS"].GetString().Trim()
            };
            var codes = material.CodeAsString.Split("-");
            material.Code = Int16.Parse(codes[2]);
            return material;
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Description))
                return false;

            return true;
        }
    }
}
