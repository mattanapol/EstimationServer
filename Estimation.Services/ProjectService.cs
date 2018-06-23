using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            project.MaterialTypeGroups = new List<ProjectMaterialType>();

            foreach (var materialGroup in project.MaterialGroups)
            {
                var materialTypeGroup = project.MaterialTypeGroups.FirstOrDefault(e => e.MaterialType == materialGroup.MaterialType);
                if (materialTypeGroup == null)
                {
                    project.MaterialTypeGroups.Add(
                        new ProjectMaterialType()
                        {
                            MaterialType = materialGroup.MaterialType,
                            ProjectMaterialGroups = new List<ProjectMaterialGroup>() { materialGroup }
                        });
                }
                else
                {
                    materialTypeGroup.ProjectMaterialGroups.Add(materialGroup);
                }
            }

            foreach (var materialTypeGroup in project.MaterialTypeGroups)
            {
                materialTypeGroup.ProjectMaterialGroups = materialTypeGroup.ProjectMaterialGroups.OrderBy(e => e.Order).ToList();
            }

            return project;
        }
    }
}
