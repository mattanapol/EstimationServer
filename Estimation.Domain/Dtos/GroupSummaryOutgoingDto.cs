using Estimation.Domain.Models;

namespace Estimation.Domain.Dtos
{
    public class GroupSummaryOutgoingDto: ProjectSummaryOutgoingDto
    {
        /// <summary>
        /// Miscellaneous information
        /// </summary>
        public Cost MiscellaneousInfo { get; set; }

        /// <summary>
        /// Transportation information
        /// </summary>
        public Cost TransportationInfo { get; set; }
    }
}
