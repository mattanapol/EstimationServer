using System;
using System.Collections.Generic;
using Estimation.Domain.Models;

namespace Estimation.Domain.Dtos
{
    /// <summary>
    /// Project information model
    /// </summary>
    public class ProjectInfoOutgoingDto
    {
        /// <summary>
        /// Project id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Project code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Last Modified date
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Project owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// General Contractor
        /// </summary>
        public string GeneralContractor { get; set; }

        /// <summary>
        /// Construction Term
        /// </summary>
        public string ConstructionTerm { get; set; }

        /// <summary>
        /// Construction Place
        /// </summary>
        public string ConstructionPlace { get; set; }

        /// <summary>
        /// Construction Scale
        /// </summary>
        public string ConstructionScale { get; set; }

        /// <summary>
        /// Kind of work
        /// </summary>
        public string KindOfWork { get; set; }

        /// <summary>
        /// Project submit by
        /// </summary>
        public string SubmitBy { get; set; }

        /// <summary>
        /// Labour cost
        /// </summary>
        public decimal LabourCost { get; set; }

        /// <summary>
        /// Currency Unit
        /// </summary>
        public string CurrencyUnit { get; set; }

        /// <summary>
        /// Miscellaneous cost
        /// </summary>
        public Cost Miscellaneous { get; set; }

        /// <summary>
        /// Transportation cost
        /// </summary>
        public Cost Transportation { get; set; }

        /// <summary>
        /// List of main materials
        /// </summary>
        public IEnumerable<ProjectMaterialGroupOutgoingDto> MaterialGroups { get; set; }

        /// <summary>
        /// Ceiling summary up to n place.
        /// </summary>
        public int CeilingSummary { get; set; }
    }
}
