using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface;
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
        IProjectSummaryService _projectSummaryService;

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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("project/{id}/summary")]
        public async Task<IActionResult> GetProjectSummary(int id)
        {
            return Ok(new ProjectSummary());
        }

        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("group/{id}/summary")]
        public async Task<IActionResult> GetGroupSummary(int id)
        {
            return Ok(new GroupSummary());
        }
    }
}