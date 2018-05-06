using System.Collections.Generic;

namespace Estimation.Domain.Dtos
{
    public class MainMaterialOutgoingDto: MaterialOutgoingBaseDto
    {
        /// <summary>
        /// Submaterial
        /// </summary>
        public IEnumerable<SubMaterialOutgoingDto> SubMaterials { get; set; }
    }
}
