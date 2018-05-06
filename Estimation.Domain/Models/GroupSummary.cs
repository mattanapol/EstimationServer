using System;

namespace Estimation.Domain.Models
{
    public class GroupSummary: ProjectSummary
    {
        /// <summary>
        /// Miscellaneous information
        /// </summary>
        public Cost MiscellaneousInfo { get; set; }

        /// <summary>
        /// Transportation information
        /// </summary>
        public Cost TransportationInfo { get; set; }

        /// <summary>
        /// Increase summary value by material
        /// </summary>
        /// <param name="projectMaterial"></param>
        public void AddByMaterial(ProjectMaterial projectMaterial)
        {
            Accessories += (int)Math.Round(projectMaterial.TotalAccessory);
            Fittings += (int)Math.Round(projectMaterial.Totalfitting);
            Painting += (int)Math.Round(projectMaterial.TotalPainting);
            Supporting += (int)Math.Round(projectMaterial.TotalSupport);
            Installation += (int)Math.Round(projectMaterial.Installation);
            MaterialPrice += (int)Math.Round(projectMaterial.TotalOfferPrice);
        }

        /// <summary>
        /// Calculate grand total
        /// </summary>
        /// <param name="decimals"></param>
        public void CalculateGrandTotal(int decimals)
        {
            Transportation = (int)Math.Round(TransportationInfo.IsUsePercentage ?
                    TransportationInfo.Percentage * Installation / 100 :
                    TransportationInfo.Manual);

            Miscellaneous = (int)Math.Round(MiscellaneousInfo.IsUsePercentage ?
                    MiscellaneousInfo.Percentage * MaterialPrice / 100 :
                    MiscellaneousInfo.Manual);

            int total = (Accessories + Fittings
                + Painting + Supporting + Installation + MaterialPrice
                + Transportation + Miscellaneous);

            int roundedTotal = (int)(Math.Round((double)total / Math.Pow(10, decimals), 0) * Math.Pow(10, decimals));

            // Adjust Miscellaneous
            Miscellaneous += roundedTotal - total;

            GrandTotal = roundedTotal;
        }
    }
}
