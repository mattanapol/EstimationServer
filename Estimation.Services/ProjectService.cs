using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMaterialGroupService _projectMaterialGroupService;

        /// <summary>
        /// Constructor of project service.
        /// </summary>
        public ProjectService(IProjectRepository projectRepository,
            IProjectMaterialGroupService projectMaterialGroupService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _projectMaterialGroupService = projectMaterialGroupService ?? throw new ArgumentNullException(nameof(projectMaterialGroupService));
        }

        /// <summary>
        /// Get project information by project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<ProjectInfo> GetProject(int projectId)
        {
            var project = await _projectRepository.GetProjectInfo(projectId);
            project.MaterialGroups = await _projectMaterialGroupService.GetAllProjectMaterial(projectId);

            return project;
        }
    }
}
