using System.Collections.Generic;
using System.Collections.ObjectModel;
using Estimation.Domain.Models;

namespace Estimation.Domain.Dtos
{
    public class ProjectMaterialGroupOutgoingDto
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
        /// Gets or sets the ceiling summary.
        /// </summary>
        /// <value>
        /// The ceiling summary.
        /// </value>
        public int CeilingSummary { get; set; }

        /// <summary>
        /// Gets or sets the labour cost.
        /// </summary>
        /// <value>
        /// The labour cost.
        /// </value>
        public decimal LabourCost { get; set; }

        /// <summary>
        /// Miscellaneous cost
        /// </summary>
        public Cost Miscellaneous { get; set; }

        /// <summary>
        /// Transportation cost
        /// </summary>
        public Cost Transportation { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }
    }
}
