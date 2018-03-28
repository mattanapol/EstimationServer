using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Cost
    /// </summary>
    public class Cost
    {
        /// <summary>
        /// Is using percentage
        /// </summary>
        public bool IsUsePercentage { get; set; }

        /// <summary>
        /// Manual cost
        /// </summary>
        public int Manual { get; set; }

        /// <summary>
        /// Percentage cost
        /// </summary>
        public decimal Percentage { get; set; }
    }
}
