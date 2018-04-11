using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class ProjectMaterialService : IProjectMaterialService
    {
        private readonly IProjectMaterialRepository _projectMaterialRepository;

        /// <summary>
        /// Project material service constructor
        /// </summary>
        /// <param name="projectMaterialRepository"></param>
        public ProjectMaterialService(IProjectMaterialRepository projectMaterialRepository)
        {
            _projectMaterialRepository = projectMaterialRepository ?? throw new ArgumentNullException(nameof(projectMaterialRepository));
        }

        /// <summary>
        /// Get project material by projact material id
        /// </summary>
        /// <param name="projectMaterialId"></param>
        /// <returns></returns>
        public async Task<ProjectMaterial> GetProjectMaterial(int projectMaterialId)
        {
            var projectMaterial = await _projectMaterialRepository.GetProjectMaterial(projectMaterialId);
            return projectMaterial;
        }
    }
}
