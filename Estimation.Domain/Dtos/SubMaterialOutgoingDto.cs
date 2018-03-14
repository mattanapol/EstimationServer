using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class SubMaterialOutgoingDto: MaterialOutgoingBaseDto
    {
        /// <summary>
        /// Submaterial
        /// </summary>
        public IEnumerable<MaterialOutgoingDto> Materials { get; set; }
    }
}
