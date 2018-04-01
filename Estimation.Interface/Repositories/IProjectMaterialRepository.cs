using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface.Repositories
{
    /// <summary>
    /// Interface of Project material repository
    /// </summary>
    public interface IProjectMaterialRepository
    {
        /// <summary>
        /// Create material and add to material group id accordingly
        /// </summary>
        /// <param name="materialGroupId">Parent material group</param>
        /// <param name="material">Project Material that going to be add</param>
        /// <returns></returns>
        Task<ProjectMaterial> CreateMaterial(int materialGroupId, ProjectMaterial material);

        /// <summary>
        /// Get project material by id
        /// </summary>
        /// <param name="id">Project material id</param>
        /// <returns></returns>
        Task<ProjectMaterial> GetProjectMaterial(int id);
    }
}
