using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Project controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/projects")]
    public class ProjectsController : EstimationBaseController
    {
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Constructor of material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="projectRepository"></param>
        public ProjectsController(ITypeMappingService typeMappingService, IProjectRepository projectRepository) : base(typeMappingService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        /// <summary>
        /// Get All Available Projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProject()
        {
            IEnumerable<ProjectInfoLightDto> projects = await _projectRepository.GetAllProjectInfo();
            return Ok(OutgoingResult<IEnumerable<ProjectInfoLightDto>>.SuccessResponse(projects));
        }

        /// <summary>
        /// Get Project by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var result = await _projectRepository.GetProjectInfo(id);
            return Ok(OutgoingResult<ProjectInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Create new project.
        /// </summary>
        /// <param name="project"></param>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]ProjectInfoIncommingDto project)
        {
            ProjectInfo projectInfo = TypeMappingService.Map<ProjectInfoIncommingDto, ProjectInfo>(project);
            var result = await _projectRepository.CreateProjectInfo(projectInfo);
            return Ok(OutgoingResult<ProjectInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Update project information by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectInfo(int id, [FromBody]ProjectInfoIncommingDto project)
        {
            ProjectInfo projectInfo = TypeMappingService.Map<ProjectInfoIncommingDto, ProjectInfo>(project);
            var result = await _projectRepository.UpdateProjectInfo(id, projectInfo);
            return Ok(OutgoingResult<ProjectInfo>.SuccessResponse(result));
        }
        
        /// <summary>
        /// Delete project by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(OutgoingResult<string>.SuccessResponse("Deleted"));
        }
    }
}
