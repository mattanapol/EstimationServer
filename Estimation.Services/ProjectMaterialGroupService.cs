using Estimation.Domain.Models;
using Estimation.Services.Interfaces;
using Estimation.Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class ProjectMaterialGroupService : IProjectMaterialGroupService
    {
        private readonly IProjectMaterialGroupRepository _projectMaterialGroupRepository;

        public ProjectMaterialGroupService(IProjectMaterialGroupRepository projectMaterialGroupRepository)
        {
            _projectMaterialGroupRepository = projectMaterialGroupRepository ?? throw new ArgumentNullException(nameof(projectMaterialGroupRepository));
        }

        public async Task<ProjectMaterialGroup> CreateProjectMaterialGroup(int projectId, ProjectMaterialGroup projectInfo)
        {
            return await _projectMaterialGroupRepository.CreateProjectMaterialGroup(projectId, projectInfo);
        }

        public async Task DeleteProjectMaterialGroup(int id)
        {
            await _projectMaterialGroupRepository.DeleteProjectMaterialGroup(id);
        }

        public async Task<IEnumerable<ProjectMaterialGroup>> GetAllProjectMaterial(int projectId)
        {
            var projectMaterialGroups = await _projectMaterialGroupRepository.GetAllProjectMaterial(projectId);
            return projectMaterialGroups;
        }

        public async Task<ProjectMaterialGroup> GetProjectMaterialGroup(int id)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.GetProjectMaterialGroup(id);
            var projectMaterialGroups = (await _projectMaterialGroupRepository.GetAllProjectMaterial(projectMaterialGroup.ProjectId))
                .Where(e => e.ParentGroupId.GetValueOrDefault(0) == id);
            projectMaterialGroup.ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroups.ToList());

            return projectMaterialGroup;
        }

        public async Task<ProjectMaterialGroup> UpdateProjectMaterialGroup(int id, ProjectMaterialGroup projectInfo)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.UpdateProjectMaterialGroup(id, projectInfo);
            return projectMaterialGroup;
        }
    }
}
