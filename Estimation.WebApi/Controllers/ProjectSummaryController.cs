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
    /// Project summary controller
    /// </summary>
    [Produces("application/json")]
    [Route("api")]
    public class ProjectSummaryController : Controller
    {
        private readonly IProjectSummaryService _projectSummaryService;

        /// <summary>
        /// Project summary controller constructor
        /// </summary>
        /// <param name="projectSummaryService"></param>
        public ProjectSummaryController(IProjectSummaryService projectSummaryService)
        {
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
        }

        /// <summary>
        /// Get project summary by project id
        /// </summary>
        /// <param name="id">Project ID</param>
        /// <returns></returns>
        [HttpGet("project/{id}/summary")]
        public async Task<IActionResult> GetProjectSummary(int id)
        {
            var result = await _projectSummaryService.GetProjectSummary(id);
            return Ok(OutgoingResult<ProjectSummary>.SuccessResponse(result));
        }

        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id">Group ID</param>
        /// <returns></returns>
        [HttpGet("group/{id}/summary")]
        public async Task<IActionResult> GetGroupSummary(int id)
        {
            var result = await _projectSummaryService.GetGroupSummary(id);
            return Ok(OutgoingResult<GroupSummary>.SuccessResponse(result));
        }

        /// <summary>
        /// Adjust group summary by group id
        /// </summary>
        /// <param name="id">Group ID</param>
        /// <param name="groupSummaryIncomingDto"></param>
        /// <returns></returns>
        [HttpPut("group/{id}/summary")]
        public async Task<IActionResult> AdjustGroupSummary(int id, [FromBody]GroupSummaryIncomingDto groupSummaryIncomingDto)
        {
            var result = await _projectSummaryService.AdjustGroupSummary(id, groupSummaryIncomingDto);
            return Ok(OutgoingResult<GroupSummary>.SuccessResponse(result));
        }
    }
}