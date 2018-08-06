using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class ProjectScopeOfWorkDb: BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is include.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is include; otherwise, <c>false</c>.
        /// </value>
        public bool IsInclude { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the scope of work group identifier.
        /// </summary>
        /// <value>
        /// The scope of work group identifier.
        /// </value>
        public int ScopeOfWorkGroupId { get; set; }

        /// <summary>
        /// Gets or sets the scope of work group.
        /// </summary>
        /// <value>
        /// The scope of work group.
        /// </value>
        public ProjectScopeOfWorkGroupDb ScopeOfWorkGroup { get; set; }
    }
}
