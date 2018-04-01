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
        /// Project code
        /// </summary>
        public string Code { get; set; }

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
        /// Ceiling summary up to n place.
        /// </summary>
        public int CeilingSummary { get; set; }
    }
}
