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
        private readonly IPrintMaterialListService _printMaterialListService;
        private readonly IExportProjectService _exportProjectService;
        private readonly IProjectService _projectService;

        /// <summary>
        /// Export controller constructor
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="printMaterialListService"></param>
        /// <param name="exportProjectService"></param>
        /// <param name="projectService"></param>
        public ExportController(ITypeMappingService typeMappingService,
            IPrintMaterialListService printMaterialListService,
            IExportProjectService exportProjectService,
            IProjectService projectService) : base(typeMappingService)
        {
            _printMaterialListService = printMaterialListService ?? throw new ArgumentNullException(nameof(printMaterialListService));
            _exportProjectService = exportProjectService ?? throw new ArgumentNullException(nameof(exportProjectService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
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
            var exportedFile = await _exportProjectService.ExportProjectEstimation(id, projectExportRequest);
            if (exportedFile == null)
                return NoContent();
            else
            {
                string exportedFileName = await GetProjectExportedFileName(id, projectExportRequest);
                string contentType = GetContentTypeFromExportRequest(projectExportRequest);
                var dataSheetStreamResult = File(exportedFile, contentType, exportedFileName);
                return dataSheetStreamResult;
            }
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

        private async Task<string> GetProjectExportedFileName(int projectId, ProjectExportRequest projectExportRequest)
        {
            var projectInfo = await _projectService.GetProject(projectId);
            string projectName = projectInfo.Name;
            string extension = projectExportRequest.GetExportFileExtension();

            if (projectExportRequest.DataSheetReport)
                return $"{projectName}_ProjectDataSheet.{extension}";
            
            if (projectExportRequest.SummaryReport || projectExportRequest.DescriptionReport)
                return $"{projectName}_ProjectEstimation.{extension}";

            return projectName;
        }

        private string GetContentTypeFromExportRequest(ProjectExportRequest projectExportRequest)
        {
            switch (projectExportRequest.ExportFileType)
            {
                case ExportFileType.Pdf:
                    return "application/pdf";
                case ExportFileType.Excel:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                default:
                    return "";
            }
        }
    }
}