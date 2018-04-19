using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface;
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

        /// <summary>
        /// Export controller constructor
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="exportService"></param>
        public ExportController(ITypeMappingService typeMappingService, IExportService exportService) : base(typeMappingService)
        {
            _exportService = exportService ?? throw new ArgumentNullException(nameof(exportService));
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
            byte[] result = await _exportService.ExportProjectToPdf(id, projectExportRequest);
            return Ok(result);
        }
    }
}