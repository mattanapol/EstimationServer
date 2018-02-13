using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class ProjectInfoIncommingDto
    {
        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project type
        /// </summary>
        public Models.Type ProjectType { get; set; }
    }
}
