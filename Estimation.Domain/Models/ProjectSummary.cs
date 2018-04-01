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
        public decimal MaterialPrice { get; set; }

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
        /// Miscellaneous price
        /// </summary>
        public decimal Miscellaneous { get; set; }

        /// <summary>
        /// Sum of installation price
        /// </summary>
        public decimal Installation { get; set; }

        /// <summary>
        /// Transportation price
        /// </summary>
        public decimal Transportation { get; set; }

        /// <summary>
        /// Overall price
        /// </summary>
        public decimal GrandTotal { get; set; }
    }
}
