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
            Material materialModel = TypeMappingService.Map<ProjectMaterialIncomingDto, Material>(product);
            var result = await _projectMaterialRepository.CreateMaterial(projectMaterialGroupId, materialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }
    }
}