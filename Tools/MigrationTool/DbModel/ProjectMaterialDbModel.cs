using System;
using System.ComponentModel;
using System.Linq;
using DbfReader;

namespace MigrationTool.DbModel
{
    public class ProjectMaterialDbModel
    {
        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets the main item digit.
        /// </summary>
        /// <value>
        /// The main item digit.
        /// </value>
        public int MainItemDigit { get; set; } = 0;

        /// <summary>
        /// Gets or sets the sub item digit.
        /// </summary>
        /// <value>
        /// The sub item digit.
        /// </value>
        public int SubItemDigit { get; set; } = 0;

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; } = 0;

        public static ProjectMaterialDbModel CreateMaterialFromRow(
            IDbfRow row)
        {
            ProjectMaterialDbModel material = new ProjectMaterialDbModel()
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
                Remark = row["REMARKS"].GetString().Trim(),
                Item = row["ITEM"].ForceString().Trim(),
                Quantity = row["QTY"].ForceInt()
            };
            if (!string.IsNullOrWhiteSpace(material.CodeAsString))
            {
                var codes = material.CodeAsString.Split('-');
                if (codes.Length == 3)
                    material.CodeAsString = $"{Int16.Parse(codes[0].Substring(1))}-{codes[1]}-{codes[2]}";
            }
            var items = material.Item.Split('.');
            if (items.Length == 2)
            {
                material.MainItemDigit = Int16.Parse(items[0]);
                material.SubItemDigit = Int16.Parse(items[1]);
            }
            return material;
        }

        public bool IsValid()
        {
            if (Name.StartsWith("***") && Name.EndsWith("***") || (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Description)))
                return false;

            return true;
        }

        public void SanitizeName()
        {
            var names = Name.Split(')');
            if (names.Length >= 2)
            {
                Name = Name.Substring(Name.IndexOf(')') + 1).Trim();
            }
        }
    }
}