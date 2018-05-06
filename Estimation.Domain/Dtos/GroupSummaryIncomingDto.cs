using Estimation.Domain.Models;

namespace Estimation.Domain.Dtos
{
    public class GroupSummaryIncomingDto
    {
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

        /// <summary>
        /// Miscellaneous information
        /// </summary>
        public Cost MiscellaneousInfo { get; set; }

        /// <summary>
        /// Transportation information
        /// </summary>
        public Cost TransportationInfo { get; set; }

        /// <summary>
        /// Create new instance with multiplied data
        /// </summary>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        public GroupSummaryIncomingDto Split(decimal multiplier)
        {
            return new GroupSummaryIncomingDto
            {
                Accessories = Accessories * multiplier,
                Fittings = Fittings * multiplier,
                Supporting = Supporting * multiplier,
                Painting = Painting * multiplier,
                Installation = Installation * multiplier,
                MiscellaneousInfo = MiscellaneousInfo,
                TransportationInfo = TransportationInfo
            };
        }
    }
}
