using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class ProjectScopeOfWorkGroupDb: BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public string MaterialType { get; set; }

        /// <summary>
        /// Gets or sets the project scope of works.
        /// </summary>
        /// <value>
        /// The project scope of works.
        /// </value>
        public IEnumerable<ProjectScopeOfWorkDb> ProjectScopeOfWorks { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the project information.
        /// </summary>
        /// <value>
        /// The project information.
        /// </value>
        public ProjectInfoDb ProjectInfo { get; set; }
    }
}
