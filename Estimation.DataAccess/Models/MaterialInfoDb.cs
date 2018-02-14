using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.DataAccess.Models
{
    /// <summary>
    /// Material Information model
    /// </summary>
    public class MaterialInfoDb
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
        public string Code { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public Type MaterialType { get; set; }
    }
}
