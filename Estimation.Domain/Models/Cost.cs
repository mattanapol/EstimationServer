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
        /// Manual cost
        /// </summary>
        public decimal Manual { get; set; }

        /// <summary>
        /// Percentage cost
        /// </summary>
        public decimal Percentage { get; set; }
    }
}
