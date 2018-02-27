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
    /// Sub material controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/material/sub")]
    public class SubMaterialController : EstimationBaseController
    {
        private readonly ISubMaterialRepository _subMaterialRepository;

        /// <summary>
        /// Constructor of material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="materialRepository"></param>
        /// <param name="mainMaterialRepository"></param>
        /// <param name="subMaterialRepository"></param>
        public SubMaterialController(ITypeMappingService typeMappingService, IMaterialRepository materialRepository,
            IMainMaterialRepository mainMaterialRepository, ISubMaterialRepository subMaterialRepository) : base(typeMappingService)
        {
            _subMaterialRepository = subMaterialRepository ?? throw new ArgumentNullException(nameof(subMaterialRepository));
        }

        /// <summary>
        /// Add sub material to specific main material id
        /// </summary>
        /// <param name="mainMaterialId"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        [HttpPost("{mainMaterialId}")]
        public async Task<IActionResult> AddSubMaterialToMainMaterial(int mainMaterialId, [FromBody]SubMaterialIncommingDto subMaterial)
        {
            MaterialInfo subMaterialModel = TypeMappingService.Map<SubMaterialIncommingDto, MaterialInfo>(subMaterial);
            var result = await _subMaterialRepository.CreateSubMaterial(mainMaterialId, subMaterialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Update sub material
        /// </summary>
        /// <param name="subMaterialId"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        [HttpPut("{subMaterialId}")]
        public async Task<IActionResult> UpdateMaterial(int subMaterialId, [FromBody]SubMaterialIncommingDto subMaterial)
        {
            MaterialInfo subMaterialModel = TypeMappingService.Map<SubMaterialIncommingDto, MaterialInfo>(subMaterial);
            var result = await _subMaterialRepository.UpdateSubMaterial(subMaterialId, subMaterialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Delete sub material by ID
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            await _subMaterialRepository.DeleteSubMaterial(id);

            return Ok();
        }

        /// <summary>
        /// Get sub material by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubMaterial(int id)
        {
            SubMaterial material = await _subMaterialRepository.GetSubMaterial(id);

            return Ok(OutgoingResult<SubMaterial>.SuccessResponse(material));
        }
    }
}