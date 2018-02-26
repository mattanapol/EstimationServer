using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    /// <summary>
    /// Material Information model
    /// </summary>
    public class MaterialInfoDb: BaseEntity
    {
        /// <summary>
        /// Material Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }
    }
}
