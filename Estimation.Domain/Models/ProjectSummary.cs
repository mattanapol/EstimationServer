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
        public int MaterialPrice { get; set; }

        /// <summary>
        /// Sum of accessories price
        /// </summary>
        public int Accessories { get; set; }

        /// <summary>
        /// Sum of fittings price
        /// </summary>
        public int Fittings { get; set; }

        /// <summary>
        /// Sum of supporting price
        /// </summary>
        public int Supporting { get; set; }

        /// <summary>
        /// Sum of painting price
        /// </summary>
        public int Painting { get; set; }

        /// <summary>
        /// Miscellaneous price
        /// </summary>
        public int Miscellaneous { get; set; }

        /// <summary>
        /// Sum of installation price
        /// </summary>
        public int Installation { get; set; }

        /// <summary>
        /// Transportation price
        /// </summary>
        public int Transportation { get; set; }

        /// <summary>
        /// Overall price
        /// </summary>
        public int GrandTotal { get; set; }
        
        /// <summary>
        /// Add by group summary
        /// </summary>
        /// <param name="groupSummary"></param>
        public void AddByGroupSummary(GroupSummary groupSummary)
        {
            Accessories += groupSummary.Accessories;
            Fittings += groupSummary.Fittings;
            Painting += groupSummary.Painting;
            Supporting += groupSummary.Supporting;
            Installation += groupSummary.Installation;
            MaterialPrice += groupSummary.MaterialPrice;
            Transportation += groupSummary.Transportation;
            Miscellaneous += groupSummary.Miscellaneous;
            GrandTotal += groupSummary.GrandTotal;
        }
    }
}
