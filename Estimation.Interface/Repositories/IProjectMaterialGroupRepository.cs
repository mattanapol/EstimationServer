﻿using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface.Repositories
{
    public interface IProjectMaterialGroupRepository
    {
        /// <summary>
        /// Get all project material group id by project id
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProjectMaterialGroup>> GetAllProjectMaterialGroupInProject(int projectId);

        /// <summary>
        /// Create project material group record to specific project id
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        Task<ProjectMaterialGroup> CreateProjectMaterialGroup(int projectId, ProjectMaterialGroup projectInfo);

        /// <summary>
        /// Get project material group by project material group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProjectMaterialGroup> GetProjectMaterialGroup(int id);

        /// <summary>
        /// Update project material group by project material group id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        Task<ProjectMaterialGroup> UpdateProjectMaterialGroup(int id, ProjectMaterialGroup projectInfo);

        /// <summary>
        /// Delete project material group by project material group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProjectMaterialGroup(int id);
    }
}