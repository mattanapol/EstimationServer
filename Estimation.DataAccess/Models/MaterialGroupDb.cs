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
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

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

        /// <summary>
        /// Miscellaneous manual cost
        /// </summary>
        public int MiscellaneousManual { get; set; }

        /// <summary>
        /// Miscellaneous percentage cost
        /// </summary>
        public decimal MiscellaneousPercentage { get; set; }

        /// <summary>
        /// Is Miscellaneous currently using percentage
        /// </summary>
        public bool MiscellaneousIsUsePercentage { get; set; }

        /// <summary>
        /// Transportation manual cost
        /// </summary>
        public int TransportationManual { get; set; }

        /// <summary>
        /// Transportation percentage cost
        /// </summary>
        public decimal TransportationPercentage { get; set; }

        /// <summary>
        /// Is Transportation currently using percentage
        /// </summary>
        public bool TransportationIsUsePercentage { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        public string Remarks { get; set; }
    }
}
