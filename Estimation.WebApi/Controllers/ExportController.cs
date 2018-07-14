using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Services;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IPrintProjectSummaryReportService = Estimation.Interface.IPrintProjectSummaryReportService;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Export controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/export")]
    public class ExportController : EstimationBaseController
    {
        private readonly IExportService _exportService;
        private readonly IPrintMaterialListService _printMaterialListService;
        private readonly IPrintProjectDatasheetService _printProjectDatasheetService;
        private readonly IPrintProjectSummaryReportService _printProjectSummaryReportService;
        private readonly IPrintProjectDescriptionReportService _printProjectDescriptionReportService;

        /// <summary>
        /// Export controller constructor
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="exportService"></param>
        /// <param name="printMaterialListService"></param>
        /// <param name="printProjectDatasheetService"></param>
        /// <param name="printProjectSummaryReportService"></param>
        /// <param name="printProjectDescriptionReportService"></param>
        public ExportController(ITypeMappingService typeMappingService,
            IExportService exportService,
            IPrintMaterialListService printMaterialListService,
            IPrintProjectDatasheetService printProjectDatasheetService,
            IPrintProjectSummaryReportService printProjectSummaryReportService,
            IPrintProjectDescriptionReportService printProjectDescriptionReportService) : base(typeMappingService)
        {
            _exportService = exportService ?? throw new ArgumentNullException(nameof(exportService));
            _printMaterialListService = printMaterialListService ?? throw new ArgumentNullException(nameof(printMaterialListService));
            _printProjectDatasheetService = printProjectDatasheetService ?? throw new ArgumentNullException(nameof(printProjectDatasheetService));
            _printProjectSummaryReportService = printProjectSummaryReportService ?? throw new ArgumentNullException(nameof(printProjectSummaryReportService));
            _printProjectDescriptionReportService = printProjectDescriptionReportService ?? throw new ArgumentNullException(nameof(printProjectDescriptionReportService));
        }

        /// <summary>
        /// Export project to PDF
        /// </summary>
        /// <param name="projectExportRequest">Project export request object</param>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        [HttpPost("project/{id}")]
        public async Task<IActionResult> ExportProject(int id,[FromBody]ProjectExportRequest projectExportRequest)
        {
            var htmls = new List<string>();

            if (projectExportRequest.DataSheetReport)
            {
                byte[] dataSheetResult = await _printProjectDatasheetService.GetProjectDatasheetAsPdf(id, projectExportRequest);
                var dataSheetStreamResult = File(dataSheetResult, "application/pdf", "ProjectDataSheet.pdf");
                return dataSheetStreamResult;
            }

            if (projectExportRequest.SummaryReport)
            {
                htmls.Add(await _printProjectSummaryReportService.GetProjectSummaryAsHtml(id, projectExportRequest));
            }
            if (projectExportRequest.DescriptionReport)
            {
                htmls.Add(await _printProjectDescriptionReportService.GetProjectDescriptionAsHtml(id, projectExportRequest));
            }

            if (htmls.Any())
            {
                byte[] result = await _exportService.ExportProjectToPdf(htmls, projectExportRequest);
                var streamResult = File(result, "application/pdf", "ProjectEstimation.pdf");
                return streamResult;
            }
            else
                return NoContent();
        }

        /// <summary>
        /// Export material list to PDF
        /// </summary>
        /// <param name="printOrderRequest">Print request object</param>
        /// <returns></returns>
        [HttpPost("material")]
        public async Task<IActionResult> ExportMaterial([FromBody]MaterialListPrintRequest printOrderRequest)
        {
            byte[] result = await _printMaterialListService.GetMaterialListAsPdf(printOrderRequest);
            var streamResult = File(result, "application/pdf", "MaterialList.pdf");
            return streamResult;
        }
    }
}