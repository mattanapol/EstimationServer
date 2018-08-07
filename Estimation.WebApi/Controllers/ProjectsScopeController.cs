using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Project controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/projects/scope")]
    public class ProjectsScopeController : EstimationBaseController
    {
        private readonly IProjectScopeService _projectScopeService;

        /// <summary>
        /// Constructor of project controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="projectScopeService"></param>
        public ProjectsScopeController(ITypeMappingService typeMappingService, IProjectScopeService projectScopeService) : base(typeMappingService)
        {
            _projectScopeService = projectScopeService ?? throw new ArgumentNullException(nameof(projectScopeService));
        }

        /// <summary>
        /// Gets the available material.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAvailableMaterial()
        {
            var availableMaterial = _projectScopeService.GetAvailableMaterialType();
            return Ok(OutgoingResult<IEnumerable<string>>.SuccessResponse(availableMaterial));
        }

        /// <summary>
        /// Gets the project scope template.
        /// </summary>
        /// <param name="materialType">Type of the material.</param>
        /// <returns></returns>
        [HttpGet("{materialType}")]
        public IActionResult GetProjectScopeTemplate(string materialType)
        {
            var result = _projectScopeService.GetProjectScopeTemplate(materialType);
            var projectScopeOfWorkGroupDto =
                TypeMappingService.Map<ProjectScopeOfWorkGroup, ProjectScopeOfWorkGroupDto>(result);
            return Ok(OutgoingResult<ProjectScopeOfWorkGroupDto>.SuccessResponse(projectScopeOfWorkGroupDto));
        }

        /// <summary>
        /// Gets the project scope of work report.
        /// </summary>
        /// <param name="projectScopeOfWorkGroupDto">The project scope of work group.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetProjectScopeOfWorkReport([FromBody]ProjectScopeOfWorkGroupDto projectScopeOfWorkGroupDto)
        {
            var projectScopeOfWorkGroup =
                TypeMappingService.Map<ProjectScopeOfWorkGroupDto, ProjectScopeOfWorkGroup>(projectScopeOfWorkGroupDto);
            var result = _projectScopeService.GetProjectScopeOfWorkReport(projectScopeOfWorkGroup);
            string contentType = "application/pdf";
            var dataSheetStreamResult = File(result, contentType, "ProjectScope.pdf");
            return dataSheetStreamResult;
        }
    }
}
