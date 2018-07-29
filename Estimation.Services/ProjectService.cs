using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;

namespace Estimation.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMaterialGroupService _projectMaterialGroupService;
        private readonly IProjectMaterialRepository _projectMaterialRepository;

        /// <summary>
        /// Constructor of project service.
        /// </summary>
        public ProjectService(IProjectRepository projectRepository,
            IProjectMaterialGroupService projectMaterialGroupService, IProjectMaterialRepository projectMaterialRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _projectMaterialGroupService = projectMaterialGroupService ?? throw new ArgumentNullException(nameof(projectMaterialGroupService));
            _projectMaterialRepository = projectMaterialRepository ?? throw new ArgumentNullException(nameof(projectMaterialRepository));
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

        /// <inheritdoc />
        public async Task<ProjectInfo> Clone(int projectId)
        {
            var originalProject = await GetProject(projectId);

            return await CreateProjectFromProjectInfo(originalProject);
        }

        public async Task<ProjectInfo> CreateProjectFromProjectInfo(ProjectInfo originalProject)
        {
            var newProject = await _projectRepository.CreateProjectInfo(originalProject);
            foreach (var originalProjectMaterialGroup in originalProject.MaterialGroups)
            {
                var newMainProjectMaterialGroup =
                    await _projectMaterialGroupService.CreateProjectMaterialGroup(newProject.Id, originalProjectMaterialGroup);

                if (originalProjectMaterialGroup.ChildGroups != null && originalProjectMaterialGroup.ChildGroups.Count() != 0)
                {
                    foreach (var originalSubGroup in originalProjectMaterialGroup.ChildGroups)
                    {
                        var newSubGroup = originalSubGroup;
                        newSubGroup.ParentGroupId = newMainProjectMaterialGroup.Id;
                        await _projectMaterialGroupService.CreateProjectMaterialGroup(newProject.Id, newSubGroup);
                    }
                }
                else if (originalProjectMaterialGroup.Materials != null)
                {
                    foreach (var originalMaterial in originalProjectMaterialGroup.Materials)
                    {
                        var newMaterial = originalMaterial;
                        await _projectMaterialRepository.CreateMaterial(newMainProjectMaterialGroup.Id, newMaterial);
                    }
                }
            }

            return newProject;
        }
    }
}
