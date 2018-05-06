using System.Collections.Generic;

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
