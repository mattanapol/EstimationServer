using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
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
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// Project material group service constructor
        /// </summary>
        /// <param name="projectMaterialGroupRepository"></param>
        public ProjectMaterialGroupService(IProjectMaterialGroupRepository projectMaterialGroupRepository, IProjectRepository projectRepository)
        {
            _projectMaterialGroupRepository = projectMaterialGroupRepository ?? throw new ArgumentNullException(nameof(projectMaterialGroupRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        /// <summary>
        /// Create project material group and add it to project by project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> CreateProjectMaterialGroup(int projectId, ProjectMaterialGroup projectInfo)
        {
            var result = await _projectMaterialGroupRepository.CreateProjectMaterialGroup(projectId, projectInfo);
            int maxOrder = 0;
            if (result.ParentGroupId.GetValueOrDefault(0) > 0)
            {
                var parentGroup = await GetProjectMaterialGroup(result.ParentGroupId.GetValueOrDefault(0));
                if (parentGroup.ChildGroups.Count() == 1)
                    return await UpdateProjectMaterialSubGroupOrder(result.Id, parentGroup.Order, 0);
                else
                {
                    maxOrder = parentGroup.ChildGroups.Max(m => m.Order);
                    return await UpdateProjectMaterialSubGroupOrder(result.Id, parentGroup.Order, maxOrder + 1);
                }
            }
            else
            {
                var materialGroups = await GetAllProjectMaterial(projectId);
                if (materialGroups.Count() == 1)
                    return await UpdateProjectMaterialGroupOrder(result.Id, 0);
                else
                {
                    maxOrder = materialGroups.Max(m => m.Order);
                    return await UpdateProjectMaterialGroupOrder(result.Id, maxOrder + 1);
                }

                
            }
            
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
        /// Updates the project material group order.
        /// </summary>
        /// <param name="id">The Project material group id.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> UpdateProjectMaterialGroupOrder(int id, int order)
        {
            var originalProjectMaterialGroup = await GetProjectMaterialGroup(id);
            if (originalProjectMaterialGroup.Order != order || order == 0)
            {
                ProjectMaterialGroup projectMaterialGroup = await _projectMaterialGroupRepository.UpdateProjectMaterialGroupOrder(id, order, (order + 1).ToString());
                if (originalProjectMaterialGroup.ChildGroups != null)
                    for (var i = 0; i < originalProjectMaterialGroup.ChildGroups.Count; i++)
                    {
                        originalProjectMaterialGroup.ChildGroups[i] = await UpdateProjectMaterialSubGroupOrder(
                            originalProjectMaterialGroup.ChildGroups[i].Id,
                            order,
                            originalProjectMaterialGroup.ChildGroups[i].Order);
                    }

                return projectMaterialGroup;
            }
            else
            {
                return originalProjectMaterialGroup;
            }
        }

        /// <summary>
        /// Updates the project material sub group order.
        /// </summary>
        /// <param name="id">The Project material sub group id.</param>
        /// <param name="parentOrder">Parent group order</param>
        /// <param name="childOrder">Sub group order</param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> UpdateProjectMaterialSubGroupOrder(int id, int parentOrder, int childOrder)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.UpdateProjectMaterialGroupOrder(id, childOrder, ($"{parentOrder + 1}-{childOrder + 1}"));

            return projectMaterialGroup;
        }


        /// <summary>
        /// Get all project material group by project id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectMaterialGroup>> GetAllProjectMaterial(int projectId)
        {
            var projectMaterialGroups = await _projectMaterialGroupRepository.GetAllProjectMaterialGroupInProject(projectId);
            var projectInfo = await _projectRepository.GetProjectInfo(projectId);
            projectMaterialGroups = projectMaterialGroups.Select(e =>
            {
                e.LabourCost = projectInfo.LabourCost;
                e.CeilingSummary = projectInfo.CeilingSummary;
                return e;
            }).ToList();
            Collection<ProjectMaterialGroup> results = new Collection<ProjectMaterialGroup>(projectMaterialGroups
                .Where(e => e.ParentGroupId.GetValueOrDefault(0) == 0)
                .OrderBy(e => e.Order)
                .ToList());
            projectMaterialGroups = projectMaterialGroups.Where(e => e.ParentGroupId.GetValueOrDefault(0) > 0);
            for(int i = 0; i<results.Count; i++)
            {
                var projectMaterialGroupChild = projectMaterialGroups
                    .Where(e => e.ParentGroupId.GetValueOrDefault(0) == results[i].Id)
                    .OrderBy(e => e.Order);
                results[i].ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroupChild.ToList());
                results[i] = CalculateMaterialFields(results[i], projectInfo);
            }
            
            return results;
        }

        /// <summary>
        /// Get project material group by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> GetProjectMaterialGroup(int id)
        {
            var projectMaterialGroup = await _projectMaterialGroupRepository.GetProjectMaterialGroup(id);
            var projectInfo = await _projectRepository.GetProjectInfo(projectMaterialGroup.ProjectId);
            projectMaterialGroup.LabourCost = projectInfo.LabourCost;
            projectMaterialGroup.CeilingSummary = projectInfo.CeilingSummary;
            projectMaterialGroup = await GetSubGroup(projectMaterialGroup, projectInfo);

            return projectMaterialGroup;
        }

        private async Task<ProjectMaterialGroup> GetSubGroup(ProjectMaterialGroup projectMaterialGroup, ProjectInfo projectInfo)
        {
            if (projectMaterialGroup == null) throw new ArgumentNullException(nameof(projectMaterialGroup));
            
            var projectMaterialGroups = (await _projectMaterialGroupRepository.GetAllProjectMaterialGroupInProject(projectMaterialGroup.ProjectId))
                .Where(e => e.ParentGroupId.GetValueOrDefault(0) == projectMaterialGroup.Id)
                .Select(e =>
                {
                    e.LabourCost = projectInfo.LabourCost;
                    e.CeilingSummary = projectInfo.CeilingSummary;
                    return e;
                });
            projectMaterialGroup.ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroups.ToList());
            
            projectMaterialGroup = CalculateMaterialFields(projectMaterialGroup, projectInfo);
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

        private ProjectMaterialGroup CalculateMaterialFields(ProjectMaterialGroup projectMaterialGroup, ProjectInfo projectInfo)
        {
            ProjectMaterialGroup newProjectMaterialGroup = projectMaterialGroup;
            if (projectMaterialGroup.ChildGroups != null && projectMaterialGroup.ChildGroups.Count > 0)
            {
                Collection<ProjectMaterialGroup> child = new Collection<ProjectMaterialGroup>();
                foreach (var childMaterialGroup in projectMaterialGroup.ChildGroups)
                {
                    child.Add(CalculateMaterialFields(childMaterialGroup, projectInfo));
                }
                newProjectMaterialGroup.ChildGroups = child;
            }
            else if (projectMaterialGroup.Materials != null && projectMaterialGroup.Materials.Count > 0)
            {
                foreach (var material in projectMaterialGroup.Materials)
                {
                    material.ProjectLabourCost = projectInfo.LabourCost;
                }
            }
            return newProjectMaterialGroup;
        }
    }
}
