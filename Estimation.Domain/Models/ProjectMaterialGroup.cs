using System;
using System.Collections.Generic;
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
        /// Parent group identification
        /// </summary>
        public int? ParentGroupId { get; set; }

        /// <summary>
        /// Materials
        /// </summary>
        public IEnumerable<Material> Materials { get; set; }

        /// <summary>
        /// Project information this group belong to
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }
}
