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
    /// Main material controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/material/main")]
    public class MainMaterialController : EstimationBaseController
    {
        private readonly IMainMaterialRepository _mainMaterialRepository;

        /// <summary>
        /// Constructor of material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="mainMaterialRepository"></param>
        public MainMaterialController(ITypeMappingService typeMappingService,
            IMainMaterialRepository mainMaterialRepository) : base(typeMappingService)
        {
            _mainMaterialRepository = mainMaterialRepository ?? throw new ArgumentNullException(nameof(mainMaterialRepository));
        }

        /// <summary>
        /// Add material to specific main material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMainMaterial([FromBody]MainMaterialIncommingDto material)
        {
            MaterialInfo materialInfo = TypeMappingService.Map<MainMaterialIncommingDto, MaterialInfo>(material);
            var result = await _mainMaterialRepository.CreateMainMaterial(materialInfo);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Update main material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mainMaterial"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterial(int id, [FromBody]MainMaterialIncommingDto mainMaterial)
        {
            MaterialInfo mainMaterialModel = TypeMappingService.Map<MainMaterialIncommingDto, MaterialInfo>(mainMaterial);
            var result = await _mainMaterialRepository.UpdateMainMaterial(id, mainMaterialModel);
            return Ok(OutgoingResult<MaterialInfo>.SuccessResponse(result));
        }

        /// <summary>
        /// Delete main material by ID
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            await _mainMaterialRepository.DeleteMainMaterial(id);

            return Ok();
        }

        /// <summary>
        /// Get main material by ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMainMaterial(int id)
        {
            MainMaterial material = await _mainMaterialRepository.GetMainMaterial(id);

            return Ok(OutgoingResult<MainMaterial>.SuccessResponse(material));
        }
    }
}