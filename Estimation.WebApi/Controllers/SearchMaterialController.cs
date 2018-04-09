using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Estimation.Interface;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Search material class
    /// </summary>
    [Produces("application/json")]
    [Route("api/search/material")]
    public class SearchMaterialController : EstimationBaseController
    {
        private readonly IMaterialService _materialService;

        /// <summary>
        /// Constructor of material controller
        /// </summary>
        /// <param name="typeMappingService"></param>
        /// <param name="materialService"></param>
        public SearchMaterialController(ITypeMappingService typeMappingService, IMaterialService materialService) : base(typeMappingService)
        {
            _materialService = materialService ?? throw new ArgumentNullException(nameof(materialService));
        }

        /// <summary>
        /// Get all materials list
        /// </summary>
        /// <param name="materialType">Material type</param>
        /// <returns></returns>
        [HttpGet("tree")]
        public async Task<IActionResult> GetAllMaterialList(string materialType)
        {
            var materials = await _materialService.GetOverallMaterial(materialType);

            return Ok(OutgoingResult<IEnumerable<MainMaterial>>.SuccessResponse(materials));
        }

        /// <summary>
        /// Get all materials list with tags
        /// </summary>
        /// <param name="materialType">Material type</param>
        /// <returns></returns>
        [HttpGet("tags")]
        public async Task<IActionResult> GetMaterialWithTags(string materialType)
        {
            var materials = await _materialService.SearchMaterialByType(materialType);

            return Ok(OutgoingResult<IEnumerable<SearchResultMaterialDto>>.SuccessResponse(materials));
        }
    }
}