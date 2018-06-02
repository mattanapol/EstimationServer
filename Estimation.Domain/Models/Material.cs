using System.Collections.Generic;
using System.Linq;

namespace Estimation.Domain.Models
{
    public class Material: MaterialInfo, IPrintable
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
        /// Gets or sets the child.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        /// <remark>Material don't have child</remark>
        public IEnumerable<IPrintable> Child => null;


        /// <inheritdoc />
        public override Dictionary<string, string> GetDataDictionary()
        {
            var baseDataDict = base.GetDataDictionary();
            var dataDict = new Dictionary<string, string>
            {
                {
                    "##LISTPRICE##", ListPrice.ToString("N")
                },
                {
                    "##NETPRICE##", NetPrice.ToString("N")
                },
                {
                    "##OFFERPRICE##", OfferPrice.ToString("N")
                },
                {
                    "##MANPOWER##", Manpower.ToString("N3")
                },
                {
                    "##FITTINGS##", Fittings.ToString("N")
                },
                {
                    "##ACCESSORY##", Accessory.ToString("N")
                },
                {
                    "##SUPPORTING##", Supporting.ToString("N")
                },
                {
                    "##PAINTING##", Painting.ToString("N")
                },
                {
                    "##REMARK##", Remark
                },
                {
                    "##UNIT##", Unit
                }
            };

            var result = baseDataDict.Combine(dataDict);
            return result;
        }

        /// <inheritdoc />
        public string TargetClass => "material";
    }
}
