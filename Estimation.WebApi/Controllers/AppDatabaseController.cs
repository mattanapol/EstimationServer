using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Interface;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Material database controller class
    /// </summary>
    [Produces("application/json")]
    [Route("api/database/material/")]
    public class AppDatabaseController : Controller
    {
        private readonly IAppDbMigrationService _materialDbMigrationService;
        /// <summary>
        /// Constructor of material database controller
        /// </summary>
        /// <param name="materialDbMigrationService"></param>
        public AppDatabaseController(IAppDbMigrationService materialDbMigrationService)
        {
            _materialDbMigrationService = materialDbMigrationService ?? throw new ArgumentNullException(nameof(materialDbMigrationService));
        }

        /// <summary>
        /// Migrate material database
        /// </summary>
        /// <returns></returns>
        [HttpGet("migrate")]
        public async Task<IActionResult> Migrate()
        {
            await _materialDbMigrationService.Migrate();

            return Ok(OutgoingResult<string>.SuccessResponse("Migrate application database complete."));
        }

        /// <summary>
        /// Migrate material database
        /// </summary>
        /// <returns></returns>
        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            await _materialDbMigrationService.Seed();

            return Ok(OutgoingResult<string>.SuccessResponse("Seed application database complete."));
        }
    }
}