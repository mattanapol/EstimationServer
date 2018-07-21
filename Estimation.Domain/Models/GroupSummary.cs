using System;
using System.Collections.Generic;
using System.Linq;

namespace Estimation.Domain.Models
{
    public class GroupSummary: ProjectSummary, IPrintable
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
        /// Gets or sets the project material group information.
        /// </summary>
        /// <value>
        /// The project material group information.
        /// </value>
        public ProjectMaterialGroup ProjectMaterialGroupInfo { get; set; }

        /// <summary>
        /// Increase summary value by material
        /// </summary>
        /// <param name="projectMaterial"></param>
        public void AddByMaterial(ProjectMaterial projectMaterial)
        {
            Accessories += (int)Math.Round(projectMaterial.TotalAccessory);
            Fittings += (int)Math.Round(projectMaterial.TotalFitting);
            Painting += (int)Math.Round(projectMaterial.TotalPainting);
            Supporting += (int)Math.Round(projectMaterial.TotalSupport);
            Installation += (int)Math.Round(projectMaterial.Installation);
            MaterialPrice += (int)Math.Round(projectMaterial.TotalOfferPrice);
            NetPrice += (int)Math.Round(projectMaterial.TotalNetPrice);
            ListPrice += (int)Math.Round(projectMaterial.TotalListPrice);
            Manpower += projectMaterial.TotalManpower;
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

            var summary = CalculateGrandTotal(decimals, MaterialPrice);
            Miscellaneous = summary.Miscellaneous;
            GrandTotal = summary.GrandTotal;

            var netSummary = CalculateGrandTotal(decimals, NetPrice);
            NetMiscellaneous = netSummary.Miscellaneous;
            NetGrandTotal = netSummary.GrandTotal;
        }

        /// <summary>
        /// Calculates the grand total.
        /// </summary>
        /// <param name="decimals">The decimals.</param>
        /// <param name="materialPrice">The material price.</param>
        /// <returns>(miscellaneous, grandTotal)</returns>
        private SummaryDto CalculateGrandTotal(int decimals, int materialPrice)
        {
            int miscellaneous = (int)Math.Round(MiscellaneousInfo.IsUsePercentage ?
                MiscellaneousInfo.Percentage * materialPrice / 100 :
                MiscellaneousInfo.Manual);

            int total = (Accessories + Fittings
                                     + Painting + Supporting + Installation + materialPrice
                                     + Transportation + miscellaneous);

            int roundedTotal = (int)(Math.Round((double)total / Math.Pow(10, decimals), 0) * Math.Pow(10, decimals));

            // Adjust Miscellaneous
            miscellaneous += roundedTotal - total;

            int grandTotal = roundedTotal;

            return new SummaryDto()
            {
                Miscellaneous = miscellaneous,
                GrandTotal = grandTotal
            };
        }

        /// <inheritdoc />
        public override Dictionary<string, string> GetDataDictionary()
        {
            var baseDataDict = base.GetDataDictionary();
            var projectMaterialGroupDataDict = ProjectMaterialGroupInfo?.GetDataDictionary();

            if (projectMaterialGroupDataDict == null)
                return baseDataDict;
            var result = baseDataDict.Combine(projectMaterialGroupDataDict);
            return result;
        }

        /// <inheritdoc />
        public override string TargetClass => ProjectMaterialGroupInfo == null
            ? "group-summary"
            : ProjectMaterialGroupInfo.ParentGroupId.GetValueOrDefault(0) == 0
                ? "group-summary"
                : "sub-group-summary";

        /// <inheritdoc />
        public override IEnumerable<IPrintable> Child
        {
            get
            {
                if (ChildSummaries.Count > 0)
                    return ChildSummaries;
                else
                {
                    return ProjectMaterialGroupInfo.Child;
                }
            }
        }

        private class SummaryDto
        {
            /// <summary>
            /// Gets or sets the miscellaneous.
            /// </summary>
            /// <value>
            /// The miscellaneous.
            /// </value>
            public int Miscellaneous { get; set; }
            /// <summary>
            /// Gets or sets the grand total.
            /// </summary>
            /// <value>
            /// The grand total.
            /// </value>
            public int GrandTotal { get; set; }
        }
    }
}
