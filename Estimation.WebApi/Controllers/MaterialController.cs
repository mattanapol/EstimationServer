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
        public MaterialController(ITypeMappingService typeMappingService, IMaterialRepository materialRepository): base(typeMappingService)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial(int id)
        {
            Material material = new Material
            {
                Id = id,
                Name = "Water-Cooled Chiller",
                Code = 1,
                ListPrice = 10,
                NetPrice = 10,
                OfferPrice = 12,
                Manpower = 100,
                Fittings = 10,
                Supporting = 10,
                Painting = 10,
                Remark = "Noted as remarks"
            };

            return Ok(OutgoingResult<Material>.SuccessResponse(material));
        }

        /// <summary>
        /// Add sub material to specific main material id
        /// </summary>
        /// <param name="mainMaterialId"></param>
        /// <param name="subMaterial"></param>
        /// <returns></returns>
        [HttpPost("sub/{mainMaterialId}")]
        public async Task<IActionResult> AddSubMaterialToMainMaterial(int mainMaterialId, [FromBody]SubMaterialIncommingDto subMaterial)
        {
            MaterialInfo subMaterialModel = TypeMappingService.Map<SubMaterialIncommingDto, MaterialInfo>(subMaterial);
            var result = await _materialRepository.CreateSubMaterial(mainMaterialId, subMaterialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Add material to specific main material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost("main")]
        public async Task<IActionResult> CreateMainMaterial([FromBody]MainMaterialIncommingDto material)
        {
            MaterialInfo materialInfo = TypeMappingService.Map<MainMaterialIncommingDto, MaterialInfo>(material);
            var result = await _materialRepository.CreateMainMaterial(materialInfo);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }
    }
}