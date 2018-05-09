using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface.Repositories
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Get all project information records
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProjectInfoLightDto>> GetAllProjectInfo();

        /// <summary>
        /// Create project information record
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        Task<ProjectInfo> CreateProjectInfo(ProjectInfo projectInfo);

        /// <summary>
        /// Get project information by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProjectInfo> GetProjectInfo(int id);

        /// <summary>
        /// Update project information by project id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        Task<ProjectInfo> UpdateProjectInfo(int id, ProjectInfo projectInfo);

        /// <summary>
        /// Delete project information by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProjectInfo(int id);

        /// <summary>
        /// Get project information with all material included
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProjectInfo> GetProjectInfoWithDetails(int id);
    }
}
