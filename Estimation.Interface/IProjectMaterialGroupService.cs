using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IProjectMaterialGroupService
    {
        /// <summary>
        /// Get all project material records by project id
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProjectMaterialGroup>> GetAllProjectMaterial(int projectId);

        /// <summary>
        /// Create project material group record to specific project id
        /// </summary>
        /// <param name="projectId"></param>
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
        /// <param name="id">The Project material group id.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        Task<ProjectMaterialGroup> UpdateProjectMaterialGroupOrder(int id, int order);

        /// <summary>
        /// Updates the project material sub group order.
        /// </summary>
        /// <param name="id">The Project material sub group id.</param>
        /// <param name="parentOrder">Parent group order</param>
        /// <param name="childOrder">Sub group order</param>
        /// <returns></returns>
        Task<ProjectMaterialGroup> UpdateProjectMaterialSubGroupOrder(int id, int parentOrder, int childOrder);
    }
}
