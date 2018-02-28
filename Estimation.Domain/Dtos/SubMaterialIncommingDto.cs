using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class SubMaterialIncommingDto
    {
        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int? Code { get; set; }
    }
}
