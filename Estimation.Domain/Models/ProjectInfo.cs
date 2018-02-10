using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Project information model
    /// </summary>
    public class ProjectInfo
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
        /// Project type
        /// </summary>
        public ProjectType ProjectType { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Last Modified date
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Project owner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gerneral Contractor
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
        public CurrencyUnit CurrencyUnit { get; set; }

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
