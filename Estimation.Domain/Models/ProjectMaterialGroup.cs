using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectMaterialGroup
    {
        /// <summary>
        /// Group identification
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Group code
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Parent group identification
        /// </summary>
        public int? ParentGroupId { get; set; }

        /// <summary>
        /// Materials
        /// </summary>
        public Collection<ProjectMaterial> Materials { get; set; }

        /// <summary>
        /// Project id that this group belong to.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Project information this group belong to
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// Child group of this material group
        /// </summary>
        public Collection<ProjectMaterialGroup> ChildGroups { get; set; }

        /// <summary>
        /// Miscellaneous cost
        /// </summary>
        public Cost Miscellaneous { get; set; }

        /// <summary>
        /// Transportation cost
        /// </summary>
        public Cost Transportation { get; set; }
    }
}
