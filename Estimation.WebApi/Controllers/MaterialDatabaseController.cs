using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Services.Interfaces;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/database/material/")]
    public class MaterialDatabaseController : Controller
    {
        private readonly IMaterialDbMigrationService _materialDbMigrationService;
        public MaterialDatabaseController(IMaterialDbMigrationService materialDbMigrationService)
        {
            _materialDbMigrationService = materialDbMigrationService ?? throw new ArgumentNullException(nameof(materialDbMigrationService));
        }

        /// <summary>
        /// Migrate material database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Migrate()
        {
            await _materialDbMigrationService.Migrate();

            return Ok(OutgoingResult<string>.SuccessResponse("Migrate material database complete."));
        }
    }
}