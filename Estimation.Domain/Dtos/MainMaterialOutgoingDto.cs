using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
