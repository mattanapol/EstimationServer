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
    /// Project material controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/ProjectMaterial")]
    public class ProjectMaterialController : EstimationBaseController
    {
        private readonly IProjectMaterialRepository _projectMaterialRepository;

        /// <summary>
        /// Constructor of project material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="projectMaterialRepository"></param>
        public ProjectMaterialController(ITypeMappingService typeMappingService, IProjectMaterialRepository projectMaterialRepository) : base(typeMappingService)
        {
            _projectMaterialRepository = projectMaterialRepository ?? throw new ArgumentNullException(nameof(projectMaterialRepository));
        }

        /// <summary>
        /// Add material to material group by material group id.
        /// </summary>
        /// <param name="projectMaterialGroupId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("{projectMaterialGroupId}")]
        public async Task<IActionResult> AddMaterialToGroupById(int projectMaterialGroupId, [FromBody]ProjectMaterialIncomingDto product)
        {
            ProjectMaterial materialModel = TypeMappingService.Map<ProjectMaterialIncomingDto, ProjectMaterial>(product);
            var result = await _projectMaterialRepository.CreateMaterial(projectMaterialGroupId, materialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Get Project material by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectMaterial(int id)
        {
            var result = await _projectMaterialRepository.GetProjectMaterial(id);
            return Ok(OutgoingResult<ProjectMaterial>.SuccessResponse(result));
        }

        /// <summary>
        /// Update project material by id.
        /// </summary>
        /// <param name="id">Project material id</param>
        /// <param name="product"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterialGroup(int id, [FromBody]ProjectMaterialIncomingDto product)
        {
            ProjectMaterial materialModel = TypeMappingService.Map<ProjectMaterialIncomingDto, ProjectMaterial>(product);
            var result = await _projectMaterialRepository.UpdateMaterial(id, materialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Delete project material by id
        /// </summary>
        /// <param name="id">Project material id</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectMaterialRepository.DeleteMaterial(id);
            return Ok(OutgoingResult<string>.SuccessResponse("Deleted"));
        }
    }
}