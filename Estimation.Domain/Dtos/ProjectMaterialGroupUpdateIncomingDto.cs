using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class ProjectMaterialGroupUpdateIncomingDto
    {
        /// <summary>
        /// Group code
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; }
    }
}
