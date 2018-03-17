using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Interface;
using Kaewsai.Utilities.Configurations.Interfaces;
using Kaewsai.Utilities.Configurations.Models;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Configuration controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/configuration")]
    public class ConfigurationController : EstimationBaseController
    {
        private readonly IConfigurationsService _configurationsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Estimation.WebApi.Controllers.ConfigurationController"/> class.
        /// </summary>
        /// <param name="typeMappingService">Type mapping service.</param>
        /// <param name="configurationsService">Configuration dict service.</param>
        public ConfigurationController(ITypeMappingService typeMappingService, IConfigurationsService configurationsService):base (typeMappingService)
        {
            _configurationsService = configurationsService ?? throw new ArgumentNullException(nameof(configurationsService));
        }

        /// <summary>
        /// Gets all configuration title.
        /// </summary>
        /// <returns>The all.</returns>
        [HttpGet("allTitle")]
        [ProducesResponseType(typeof(OutgoingResult<IEnumerable<string>>), 200)]
        public async Task<IActionResult> GetAllTitle()
        {
            var titleList = await _configurationsService.GetAllConfigurationTitle();
            return Ok(OutgoingResult<IEnumerable<string>>.SuccessResponse(titleList));
        }

        /// <summary>
        /// Gets all configurations.
        /// </summary>
        /// <returns>The all.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var configurationList = await _configurationsService.GetAllConfiguration();
            var outgoingContents = TypeMappingService
                .Map<IEnumerable<ConfigurationDict>, IEnumerable<ConfigurationDictDto>>(configurationList);

            return Ok(OutgoingResult<IEnumerable<ConfigurationDictDto>>.SuccessResponse(outgoingContents));
        }

        /// <summary>
        /// Get the specified configuration by title.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="title">Title.</param>
        [HttpGet("{title}")]
        [ProducesResponseType(typeof(OutgoingResult<ConfigurationDictDto>), 200)]
        public async Task<IActionResult> Get(string title)
        {
            if (string.IsNullOrEmpty(title))
                return NotFound();
            var configurationDict = await _configurationsService
                .GetConfigurationByTitle(title);
            var result = TypeMappingService
                .Map<ConfigurationDict, ConfigurationDictDto>(configurationDict);
            return Ok(OutgoingResult<ConfigurationDictDto>.SuccessResponse(result));
        }

        /// <summary>
        /// Create configuration.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="configuration">Configuration.</param>
        [HttpPost]
        [ProducesResponseType(typeof(OutgoingResult<ConfigurationDictDto>), 200)]
        public async Task<IActionResult> Post([FromBody]ConfigurationDictDto configuration)
        {
            var config = TypeMappingService
                .Map<ConfigurationDictDto, ConfigurationDict>(configuration);
            var createdConfig = await _configurationsService
                .CreateConfiguration(config);
            var result = TypeMappingService
                .Map<ConfigurationDict, ConfigurationDictDto>(createdConfig);
            return Ok(OutgoingResult<ConfigurationDictDto>.SuccessResponse(result));
        }

        /// <summary>
        /// Update the specified configuration by title.
        /// </summary>
        /// <returns>The put.</returns>
        /// <param name="title">Title.</param>
        /// <param name="configuration">Configuration.</param>
        [HttpPut("{title}")]
        [ProducesResponseType(typeof(OutgoingResult<ConfigurationDictDto>), 200)]
        public async Task<IActionResult> Put(string title, [FromBody]ConfigurationDictDto configuration)
        {
            var configurationDict = await _configurationsService
                .UpdateConfiguration(title, TypeMappingService.Map<ConfigurationDictDto, ConfigurationDict>(configuration));
            var result = TypeMappingService
                .Map<ConfigurationDict, ConfigurationDictDto>(configurationDict);
            return Ok(OutgoingResult<ConfigurationDictDto>.SuccessResponse(result));
        }
    }
}