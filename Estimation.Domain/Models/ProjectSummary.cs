using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Project summary
    /// </summary>
    public class ProjectSummary
    {
        /// <summary>
        /// Sum of material price
        /// </summary>
        int MaterialPrice { get; set; }

        /// <summary>
        /// Sum of accessories price
        /// </summary>
        int Accessories { get; set; }

        /// <summary>
        /// Sum of supporting price
        /// </summary>
        int Supporting { get; set; }

        /// <summary>
        /// Sum of painting price
        /// </summary>
        int Painting { get; set; }

        /// <summary>
        /// Miscellaneous price
        /// </summary>
        int Miscellaneous { get; set; }

        /// <summary>
        /// Sum of installation price
        /// </summary>
        int Installation { get; set; }

        /// <summary>
        /// Transportation price
        /// </summary>
        int Transportation { get; set; }

        /// <summary>
        /// Overall price
        /// </summary>
        int GrandTotal { get; set; }
    }
}
