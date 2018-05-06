using Estimation.Domain.Models;

namespace Estimation.Domain.Dtos
{
    public class AdjustSummaryRatioDto
    {
        /// <summary>
        /// Create new instance of adjust summary ratio
        /// Values will be -1 if original summary is 0
        /// </summary>
        /// <param name="originalGroupSummary"></param>
        /// <param name="newGroupSummary"></param>
        public AdjustSummaryRatioDto(GroupSummary originalGroupSummary, GroupSummaryIncomingDto newGroupSummary)
        {
            Accessories = originalGroupSummary.Accessories == 0 ? -1 : (decimal)newGroupSummary.Accessories / (decimal)originalGroupSummary.Accessories;
            Fittings = originalGroupSummary.Fittings == 0 ? -1 : (decimal)newGroupSummary.Fittings / (decimal)originalGroupSummary.Fittings;
            Supporting = originalGroupSummary.Supporting == 0 ? -1 : (decimal)newGroupSummary.Supporting / (decimal)originalGroupSummary.Supporting;
            Painting = originalGroupSummary.Painting == 0 ? -1 : (decimal)newGroupSummary.Painting / (decimal)originalGroupSummary.Painting;
            Installation = originalGroupSummary.Installation == 0 ? -1 : (decimal)newGroupSummary.Installation / (decimal)originalGroupSummary.Installation;
        }

        /// <summary>
        /// Sum of accessories price
        /// </summary>
        public decimal Accessories { get; set; }

        /// <summary>
        /// Sum of fittings price
        /// </summary>
        public decimal Fittings { get; set; }

        /// <summary>
        /// Sum of supporting price
        /// </summary>
        public decimal Supporting { get; set; }

        /// <summary>
        /// Sum of painting price
        /// </summary>
        public decimal Painting { get; set; }

        /// <summary>
        /// Sum of installation price
        /// </summary>
        public decimal Installation { get; set; }
    }
}
