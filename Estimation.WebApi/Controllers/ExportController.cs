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

        /// <summary>
        /// Export controller constructor
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="exportService"></param>
        /// <param name="printMaterialListService"></param>
        /// <param name="printProjectDatasheetService"></param>
        public ExportController(ITypeMappingService typeMappingService,
            IExportService exportService,
            IPrintMaterialListService printMaterialListService,
            IPrintProjectDatasheetService printProjectDatasheetService) : base(typeMappingService)
        {
            _exportService = exportService ?? throw new ArgumentNullException(nameof(exportService));
            _printMaterialListService = printMaterialListService ?? throw new ArgumentNullException(nameof(printMaterialListService));
            _printProjectDatasheetService = printProjectDatasheetService ?? throw new ArgumentNullException(nameof(printProjectDatasheetService));
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
            byte[] result = await _printProjectDatasheetService.GetProjectDatasheetAsPdf(id, projectExportRequest);
            var streamResult = File(result, "application/pdf", "ProjectDataSheet.pdf");
            return streamResult;
        }

        /// <summary>
        /// Export material list to PDF
        /// </summary>
        /// <param name="printOrderRequest">Print request object</param>
        /// <returns></returns>
        [HttpPost("material")]
        public async Task<IActionResult> ExportMaterial([FromBody]PrintOrderRequest printOrderRequest)
        {
            byte[] result = await _printMaterialListService.GetMaterialListAsPdf(printOrderRequest);
            var streamResult = File(result, "application/pdf", "MaterialList.pdf");
            return streamResult;
        }
    }
}