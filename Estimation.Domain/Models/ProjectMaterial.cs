using System.Collections.Generic;
using System.Linq;

namespace Estimation.Domain.Models
{
    public class ProjectMaterial: Material, IPrintable
    {
        /// <summary>
        /// Gets or sets the project labour cost.
        /// </summary>
        public decimal ProjectLabourCost { get; set; }

        /// <summary>
        /// Labour cost (Manday x ProjectLabour)
        /// </summary>
        public decimal LabourCost => Manpower * ProjectLabourCost;

        /// <summary>
        /// Installation cost (LabourCost x Quantity)
        /// </summary>
        public decimal Installation => LabourCost * Quantity;

        /// <summary>
        /// Quantity of material
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Total cost of this material
        /// </summary>
        public decimal TotalCost => TotalOfferPrice + Installation + TotalAccessory + TotalFitting + TotalPainting + TotalSupport;

        /// <summary>
        /// Total offer price
        /// </summary>
        public decimal TotalOfferPrice => OfferPrice * Quantity;

        /// <summary>
        /// Total net price
        /// </summary>
        public decimal TotalNetPrice => NetPrice * Quantity;

        /// <summary>
        /// Total accessory cost
        /// </summary>
        public decimal TotalAccessory => Accessory * Quantity;

        /// <summary>
        /// Total fitting cost
        /// </summary>
        public decimal TotalFitting => Fittings* Quantity;

        /// <summary>
        /// Total support cost
        /// </summary>
        public decimal TotalSupport => Supporting * Quantity;

        /// <summary>
        /// Total painting cost
        /// </summary>
        public decimal TotalPainting => Painting * Quantity;

        /// <summary>
        /// Gets or sets the total list price.
        /// </summary>
        /// <value>
        /// The total list price.
        /// </value>
        public decimal TotalListPrice => ListPrice * Quantity;

        /// <summary>
        /// Gets or sets the total net cost.
        /// </summary>
        /// <value>
        /// The total net cost.
        /// </value>
        public decimal TotalNetCost => TotalNetPrice + Installation + TotalAccessory + TotalFitting + TotalPainting + TotalSupport;

        /// <summary>
        /// Gets the total offer price and installation.
        /// </summary>
        /// <value>
        /// The total offer price and installation.
        /// </value>
        public decimal TotalOfferPriceAndInstallation => TotalOfferPrice + Installation;

        /// <summary>
        /// Gets or sets the total manpower.
        /// </summary>
        /// <value>
        /// The total manpower.
        /// </value>
        public decimal TotalManpower => Manpower * Quantity;

        /// <inheritdoc />
        public override Dictionary<string, string> GetDataDictionary()
        {
            var baseDataDict = base.GetDataDictionary();
            var dataDict = new Dictionary<string, string>
            {
                {
                    "LABOURCOST", LabourCost.ToString("N")
                },
                {
                    "INSTALLATION", Installation.ToString("N")
                },
                {
                    "QUANTITY", Quantity.ToString()
                },
                {
                    "TOTALCOST", TotalCost.ToString("N")
                },
                {
                    "TOTALNETCOST", TotalNetCost.ToString("N")
                },
                {
                    "TOTALOFFERPRICE", TotalOfferPrice.ToString("N")
                },
                {
                    "TOTALNETPRICE", TotalNetPrice.ToString("N")
                },
                {
                    "TOTALLISTPRICE", TotalListPrice.ToString("N")
                },
                {
                    "TOTALACCESSORY", TotalAccessory.ToString("N")
                },
                {
                    "TOTALFITTING", TotalFitting.ToString("N")
                },
                {
                    "TOTALSUPPORT", TotalSupport.ToString("N")
                },
                {
                    "TOTALPAINTING", TotalPainting.ToString("N")
                },
                {
                    "TOTALMANPOWER", TotalManpower.ToString("N")
                },
                {
                    "TotalOfferPriceAndInstallation", TotalOfferPriceAndInstallation.ToString("N")
                },
            };

            var result = baseDataDict.Combine(dataDict);
            return result;
        }
    }
}
