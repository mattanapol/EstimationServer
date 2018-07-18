namespace Estimation.Domain.Models
{
    public class ProjectExportRequest: PrintOrderRequest
    {
        /// <summary>
        /// Submit form
        /// </summary>
        public SubmitForm SubmitForm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [summary report].
        /// </summary>
        public bool SummaryReport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [description report].
        /// </summary>
        public bool DescriptionReport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [data sheet report].
        /// </summary>
        public bool DataSheetReport { get; set; }

        /// <summary>
        /// Gets or sets the type of the export file.
        /// </summary>
        /// <value>
        /// The type of the export file.
        /// </value>
        public ExportFileType ExportFileType { get; set; }

        /// <summary>
        /// Gets the export file extension.
        /// </summary>
        /// <returns></returns>
        public string GetExportFileExtension()
        {
            switch (ExportFileType)
            {
                case ExportFileType.Pdf:
                    return "pdf";
                case ExportFileType.Excel:
                    return "xlsx";
                default:
                    return "";
            }
        }
    }
}
