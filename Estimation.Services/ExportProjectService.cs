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
        private readonly IProjectSummaryService _projectSummaryService;

        public ExportProjectService(IPdfService exportService, 
            IPrintProjectDatasheetService printProjectDatasheetService,
            IPrintProjectSummaryReportService printProjectSummaryReportService,
            IPrintProjectDescriptionReportService printProjectDescriptionReportService, IProjectSummaryService projectSummaryService)
        {
            _exportService = exportService ?? throw new ArgumentNullException(nameof(exportService));
            _printProjectDatasheetService = printProjectDatasheetService ?? throw new ArgumentNullException(nameof(printProjectDatasheetService));
            _printProjectSummaryReportService = printProjectSummaryReportService ?? throw new ArgumentNullException(nameof(printProjectSummaryReportService));
            _printProjectDescriptionReportService = printProjectDescriptionReportService ?? throw new ArgumentNullException(nameof(printProjectDescriptionReportService));
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
        }

        public async Task<byte[]> ExportProjectEstimation(int projectId, ProjectExportRequest printOrder)
        {
            var projectSummary = await _projectSummaryService.GetProjectSummary(projectId);
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
                switch (printOrder.SubmitForm)
                {
                    case SubmitForm.SubmitForm:
                        var estimationSubmitForm = new EstimationSubmitForm();
                        return estimationSubmitForm.ExportToExcel(projectSummary, printOrder);
                    case SubmitForm.MaterialAndLabourCostForm:
                        var estimationDetailForm = new EstimationDetailForm();
                        return estimationDetailForm.ExportToExcel(projectSummary, printOrder);
                    case SubmitForm.NetForm:
                        var estimationNetForm = new EstimationNetForm();
                        return estimationNetForm.ExportToExcel(projectSummary, printOrder);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            return null;
        }
    }
}
