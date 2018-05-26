﻿using System;
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

        /// <inheritdoc />
        public override Dictionary<string, string> GetDataDictionary()
        {
            var baseDataDict = base.GetDataDictionary();
            var projectMaterialGroupDataDict = ProjectMaterialGroupInfo.GetDataDictionary();

            var result = baseDataDict.Concat(projectMaterialGroupDataDict).GroupBy(d => d.Key)
                .ToDictionary(d => d.Key, d => d.First().Value);
            return result;
        }

        /// <inheritdoc />
        public override string TargetClass => ProjectMaterialGroupInfo == null
            ? "group-summary"
            : ProjectMaterialGroupInfo.ParentGroupId == 0
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
    }
}
