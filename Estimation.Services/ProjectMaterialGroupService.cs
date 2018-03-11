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
    /// <summary>
    /// Project material group service class
    /// </summary>
    public class ProjectMaterialGroupService : IProjectMaterialGroupService
    {
        private readonly IProjectMaterialGroupRepository _projectMaterialGroupRepository;

        /// <summary>
        /// Project material group service constructor
        /// </summary>
        /// <param name="projectMaterialGroupRepository"></param>
        public ProjectMaterialGroupService(IProjectMaterialGroupRepository projectMaterialGroupRepository)
        {
            _projectMaterialGroupRepository = projectMaterialGroupRepository ?? throw new ArgumentNullException(nameof(projectMaterialGroupRepository));
        }

        /// <summary>
        /// Create project material group and add it to project by project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> CreateProjectMaterialGroup(int projectId, ProjectMaterialGroup projectInfo)
        {
            return await _projectMaterialGroupRepository.CreateProjectMaterialGroup(projectId, projectInfo);
        }

        /// <summary>
        /// Delete project material group by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProjectMaterialGroup(int id)
        {
            await _projectMaterialGroupRepository.DeleteProjectMaterialGroup(id);
        }

        /// <summary>
        /// Get all project material group by project id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectMaterialGroup>> GetAllProjectMaterial(int projectId)
        {
            var projectMaterialGroups = await _projectMaterialGroupRepository.GetAllProjectMaterial(projectId);
            return projectMaterialGroups;
        }

        /// <summary>
        /// Get project material group by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> GetProjectMaterialGroup(int id)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.GetProjectMaterialGroup(id);
            var projectMaterialGroups = (await _projectMaterialGroupRepository.GetAllProjectMaterial(projectMaterialGroup.ProjectId))
                .Where(e => e.ParentGroupId.GetValueOrDefault(0) == id);
            projectMaterialGroup.ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroups.ToList());

            return projectMaterialGroup;
        }

        /// <summary>
        /// Update project material group.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> UpdateProjectMaterialGroup(int id, ProjectMaterialGroup projectInfo)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.UpdateProjectMaterialGroup(id, projectInfo);
            return projectMaterialGroup;
        }
    }
}
