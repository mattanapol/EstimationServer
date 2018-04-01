using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
