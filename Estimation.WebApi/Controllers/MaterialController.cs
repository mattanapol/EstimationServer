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
    /// Material controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/material")]
    public class MaterialController : EstimationBaseController
    {
        private readonly IMaterialRepository _materialRepository;

        /// <summary>
        /// Constructor of material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="materialRepository"></param>
        /// <param name="mainMaterialRepository"></param>
        /// <param name="subMaterialRepository"></param>
        public MaterialController(ITypeMappingService typeMappingService, IMaterialRepository materialRepository, 
            IMainMaterialRepository mainMaterialRepository, ISubMaterialRepository subMaterialRepository): base(typeMappingService)
        {
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        }

        /// <summary>
        /// Get all materials list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMaterialList()
        {
            var materials = await _materialRepository.GetMaterialList();

            return Ok(OutgoingResult<IEnumerable<MainMaterial>>.SuccessResponse(materials));
        }

        /// <summary>
        /// Get material by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            Material material = await _materialRepository.GetMaterial(id);

            return Ok(OutgoingResult<Material>.SuccessResponse(material));
        }

        /// <summary>
        /// Add product to specific sub material id
        /// </summary>
        /// <param name="subMaterialId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("product/{subMaterialId}")]
        public async Task<IActionResult> AddMaterialToSubMaterial(int subMaterialId, [FromBody]MaterialIncommingDto product)
        {
            Material materialModel = TypeMappingService.Map<MaterialIncommingDto, Material>(product);
            var result = await _materialRepository.CreateMaterial(subMaterialId, materialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Update material
        /// </summary>
        /// <param name="subMaterialId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("product/{subMaterialId}")]
        public async Task<IActionResult> UpdateMaterial(int subMaterialId, [FromBody]MaterialIncommingDto product)
        {
            Material materialModel = TypeMappingService.Map<MaterialIncommingDto, Material>(product);
            var result = await _materialRepository.UpdateMaterial(subMaterialId, materialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Delete material by ID
        /// </summary>
        /// <returns></returns>
        [HttpDelete("product/{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            await _materialRepository.DeleteMaterial(id);

            return Ok();
        }
    }
}