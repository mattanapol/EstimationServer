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
    /// Project material group controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/project/group")]
    public class ProjectMaterialGroupController : EstimationBaseController
    {
        private readonly IProjectMaterialGroupRepository _projectMaterialGroupRepository;

        /// <summary>
        /// Constructor of project material group controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="projectMaterialGroupRepository"></param>
        public ProjectMaterialGroupController(ITypeMappingService typeMappingService, IProjectMaterialGroupRepository projectMaterialGroupRepository) : base(typeMappingService)
        {
            _projectMaterialGroupRepository = projectMaterialGroupRepository ?? throw new ArgumentNullException(nameof(projectMaterialGroupRepository));
        }

        /// <summary>
        /// Create project material group and add it to supplied project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectMaterialGroup"></param>
        /// <returns></returns>
        [HttpPost("{projectId}")]
        public async Task<IActionResult> CreateProjectMaterialGroup(int projectId, [FromBody]ProjectMaterialGroupIncomingDto projectMaterialGroup)
        {
            var projectMaterialGroupInfo = TypeMappingService.Map<ProjectMaterialGroupIncomingDto, ProjectMaterialGroup>(projectMaterialGroup);
            var result = await _projectMaterialGroupRepository.CreateProjectMaterialGroup(projectId, projectMaterialGroupInfo);
            return Ok(OutgoingResult<ProjectMaterialGroup>.SuccessResponse(result));
        }

        /// <summary>
        /// Get material group by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialGroup(int id)
        {
            var materialGroup = await _projectMaterialGroupRepository.GetProjectMaterialGroup(id);

            return Ok(OutgoingResult<ProjectMaterialGroup>.SuccessResponse(materialGroup));
        }


        /// <summary>
        /// Update material group by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="project"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterialGroup(int id, [FromBody]ProjectMaterialGroupUpdateIncomingDto project)
        {
            ProjectMaterialGroup projectInfo = TypeMappingService.Map<ProjectMaterialGroupUpdateIncomingDto, ProjectMaterialGroup>(project);
            var result = await _projectMaterialGroupRepository.UpdateProjectMaterialGroup(id, projectInfo);
            return Ok(OutgoingResult<ProjectMaterialGroup>.SuccessResponse(result));
        }

        /// <summary>
        /// Delete material group by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectMaterialGroupRepository.DeleteProjectMaterialGroup(id);
            return Ok(OutgoingResult<string>.SuccessResponse("Deleted"));
        }
    }
}