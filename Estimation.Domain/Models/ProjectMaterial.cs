using System.Collections.Generic;
using System.Linq;

namespace Estimation.Domain.Models
{
    public class ProjectMaterial: Material, IPrintable
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
        public decimal TotalFitting { get; set; }

        /// <summary>
        /// Total support cost
        /// </summary>
        public decimal TotalSupport { get; set; }

        /// <summary>
        /// Total painting cost
        /// </summary>
        public decimal TotalPainting { get; set; }

        /// <inheritdoc />
        public override Dictionary<string, string> GetDataDictionary()
        {
            var baseDataDict = base.GetDataDictionary();
            var dataDict = new Dictionary<string, string>
            {
                {
                    "##LABOURCOST##", LabourCost.ToString("N")
                },
                {
                    "##INSTALLATION##", Installation.ToString("N")
                },
                {
                    "##QUANTITY##", Quantity.ToString("N")
                },
                {
                    "##TOTALCOST##", TotalCost.ToString("N")
                },
                {
                    "##TOTALOFFERPRICE##", TotalOfferPrice.ToString("N")
                },
                {
                    "##TOTALACCESSORY##", TotalAccessory.ToString("N")
                },
                {
                    "##TOTALFITTING##", TotalFitting.ToString("N")
                },
                {
                    "##TOTALSUPPORT##", TotalSupport.ToString("N")
                },
                {
                    "##TOTALPAINTING##", TotalPainting.ToString("N")
                }
            };

            var result = baseDataDict.Concat(dataDict).GroupBy(d => d.Key)
                .ToDictionary(d => d.Key, d => d.First().Value);
            return result;
        }
    }
}
