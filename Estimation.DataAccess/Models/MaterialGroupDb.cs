using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class MaterialGroupDb: BaseEntity
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
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Project Id this group belong to
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Project information this group belong to
        /// </summary>
        public ProjectInfoDb ProjectInfo { get; set; }

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
        public IEnumerable<ProjectMaterialDb> Materials { get; set; }
    }
}
