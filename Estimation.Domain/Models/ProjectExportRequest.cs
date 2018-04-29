using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class ProjectExportRequest: PrintOrderRequest
    {
        /// <summary>
        /// Submit form
        /// </summary>
        public SubmitForm SubmitForm { get; set; }

        /// <summary>
        /// Submit report
        /// </summary>
        public SubmitReport SubmitReport { get; set; }
    }
}
