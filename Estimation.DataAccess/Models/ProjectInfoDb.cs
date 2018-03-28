using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    public class ProjectInfoDb: BaseEntity
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
        /// Material groups
        /// </summary>
        public IEnumerable<MaterialGroupDb> MaterialGroups { get; set; }
    }
}
