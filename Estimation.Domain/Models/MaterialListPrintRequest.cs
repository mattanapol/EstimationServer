using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class MaterialListPrintRequest : PrintOrderRequest
    {
        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public IEnumerable<string> MaterialTypes { get; set; }
    }
}
