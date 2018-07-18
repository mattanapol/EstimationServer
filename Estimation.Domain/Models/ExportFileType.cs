using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Estimation.Domain.Models
{
    public enum ExportFileType
    {
        /// <summary>
        /// PDF file
        /// </summary>
        [Description("pdf")]
        Pdf,
        /// <summary>
        /// Excel file
        /// </summary>
        [Description("xlsx")]
        Excel
    }
}
