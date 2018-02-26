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
    [Produces("application/json")]
    [Route("api/material/main")]
    public class MainMaterialController : EstimationBaseController
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMainMaterialRepository _mainMaterialRepository;
        private readonly ISubMaterialRepository _subMaterialRepository;

        /// <summary>
        /// Constructor of material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="materialRepository"></param>
        /// <param name="mainMaterialRepository"></param>
        /// <param name="subMaterialRepository"></param>
        public MainMaterialController(ITypeMappingService typeMappingService, IMaterialRepository materialRepository,
            IMainMaterialRepository mainMaterialRepository, ISubMaterialRepository subMaterialRepository) : base(typeMappingService)
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
    }
}