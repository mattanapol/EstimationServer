using Estimation.Domain.Models;
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

        /// <summary>
        /// Updates the project material group order.
        /// </summary>
        /// <param name="id">The project material group id.</param>
        /// <param name="order">The order.</param>
        /// <param name="groupCode">The group code.</param>
        /// <returns></returns>
        Task<ProjectMaterialGroup> UpdateProjectMaterialGroupOrder(int id, int order, string groupCode);
    }
}
