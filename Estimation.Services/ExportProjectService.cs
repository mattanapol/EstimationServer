using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Excel;
using Estimation.Interface;
using Kaewsai.Excel;

namespace Estimation.Services
{
    public class ExportProjectService : IExportProjectService
    {
        private readonly IPdfService _exportService;
        private readonly IPrintProjectDatasheetService _printProjectDatasheetService;
        private readonly IPrintProjectSummaryReportService _printProjectSummaryReportService;
        private readonly IPrintProjectDescriptionReportService _printProjectDescriptionReportService;

        public ExportProjectService(IPdfService exportService, 
            IPrintProjectDatasheetService printProjectDatasheetService,
            IPrintProjectSummaryReportService printProjectSummaryReportService,
            IPrintProjectDescriptionReportService printProjectDescriptionReportService)
        {
            _exportService = exportService ?? throw new ArgumentNullException(nameof(exportService));
            _printProjectDatasheetService = printProjectDatasheetService ?? throw new ArgumentNullException(nameof(printProjectDatasheetService));
            _printProjectSummaryReportService = printProjectSummaryReportService ?? throw new ArgumentNullException(nameof(printProjectSummaryReportService));
            _printProjectDescriptionReportService = printProjectDescriptionReportService ?? throw new ArgumentNullException(nameof(printProjectDescriptionReportService));
        }

        public async Task<byte[]> ExportProjectEstimation(int projectId, ProjectExportRequest printOrder)
        {
            if (printOrder.ExportFileType == ExportFileType.Pdf)
            {
                if (printOrder.DataSheetReport)
                {
                    return await _printProjectDatasheetService.GetProjectDatasheetAsPdf(projectId, printOrder);
                }

                var htmls = new List<string>();
                if (printOrder.SummaryReport)
                {
                    htmls.Add(await _printProjectSummaryReportService.GetProjectSummaryAsHtml(projectId, printOrder));
                }
                if (printOrder.DescriptionReport)
                {
                    htmls.Add(await _printProjectDescriptionReportService.GetProjectDescriptionAsHtml(projectId, printOrder));
                }

                if (htmls.Any())
                {
                    return await _exportService.ExportProjectToPdf(htmls, printOrder);
                }
            }
            else if (printOrder.ExportFileType == ExportFileType.Excel)
            {
                var workBooks = new List<byte[]>();

                if (printOrder.DataSheetReport)
                    return null;

                if (printOrder.SummaryReport)
                    workBooks.Add(await _printProjectSummaryReportService.GetProjectSummaryAsExcel(projectId, printOrder));

                if (printOrder.DescriptionReport)
                    workBooks.Add(await _printProjectDescriptionReportService.GetProjectDescriptionAsExcel(projectId, printOrder));

                if (workBooks.Any())
                    return SheetMerger.Merge(workBooks);
            }

            return null;
        }
    }
}
