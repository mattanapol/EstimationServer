﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// Project controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        /// <summary>
        /// Get All Available Projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProject()
        {
            IList<ProjectInfo> projects = new List<ProjectInfo>
            {
                new ProjectInfo(){Id = 1, Name = "Example Project", CreatedDate = DateTime.Now, ProjectType = Domain.Models.Type.Mechanic },
                new ProjectInfo(){Id = 2, Name = "Example Project2", CreatedDate = DateTime.Now, ProjectType = Domain.Models.Type.Mechanic }
            };
            return Ok(OutgoingResult<IList<ProjectInfo>>.SuccessResponse(projects));
        }
        
        /// <summary>
        /// Create new project.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]ProjectInfoIncommingDto value)
        {
            var result = new ProjectInfo() { Id = 1, Name = value.Name, CreatedDate = DateTime.Now, ProjectType = value.ProjectType };
            return Ok(OutgoingResult<ProjectInfo>.SuccessResponse(result));
        }
        
        /// <summary>
        /// Edit project information by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProject(int id, [FromBody]ProjectInfoIncommingDto value)
        {
            var result = new ProjectInfo() { Id = 1, Name = value.Name, CreatedDate = DateTime.Now, ProjectType = value.ProjectType };
            return Ok(OutgoingResult<ProjectInfo>.SuccessResponse(result));
        }
        
        /// <summary>
        /// Delete project by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(OutgoingResult<string>.SuccessResponse("Deleted"));
        }
    }
}
