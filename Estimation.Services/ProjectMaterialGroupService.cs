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
        /// Updates the project material group order.
        /// </summary>
        /// <param name="id">The Project material group id.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        public async Task<ProjectMaterialGroup> UpdateProjectMaterialGroupOrder(int id, int order)
        {
            var originalProjectMaterialGroup = await GetProjectMaterialGroup(id);
            if (originalProjectMaterialGroup.Order != order)
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
            Collection<ProjectMaterialGroup> results = new Collection<ProjectMaterialGroup>(projectMaterialGroups.Where(e => e.ParentGroupId.GetValueOrDefault(0) == 0).ToList());
            projectMaterialGroups = projectMaterialGroups.Where(e => e.ParentGroupId.GetValueOrDefault(0) > 0);
            foreach (var result in results)
            {
                var projectMaterialGroupChild = projectMaterialGroups.Where(e => e.ParentGroupId.GetValueOrDefault(0) == result.Id);
                result.ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroupChild.ToList());
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
            projectMaterialGroup = await GetSubGroup(projectMaterialGroup);

            return projectMaterialGroup;
        }

        private async Task<ProjectMaterialGroup> GetSubGroup(ProjectMaterialGroup projectMaterialGroup)
        {
            if (projectMaterialGroup == null) throw new ArgumentNullException(nameof(projectMaterialGroup));
            var projectInfo = await _projectRepository.GetProjectInfo(projectMaterialGroup.ProjectId);
            var projectMaterialGroups = (await _projectMaterialGroupRepository.GetAllProjectMaterialGroupInProject(projectMaterialGroup.ProjectId))
                .Where(e => e.ParentGroupId.GetValueOrDefault(0) == projectMaterialGroup.Id)
                .Select(e =>
                {
                    e.ProjectInfo = projectInfo;
                    return e;
                });
            projectMaterialGroup.ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroups.ToList());
            projectMaterialGroup.ProjectInfo = projectInfo;

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
                    material.LabourCost = material.Manpower * projectInfo.LabourCost;
                    material.Installation = material.LabourCost * material.Quantity;
                    
                    material.TotalOfferPrice = material.OfferPrice * material.Quantity;
                    material.TotalNetPrice = material.NetPrice * material.Quantity;
                    material.TotalListPrice = material.ListPrice * material.Quantity;

                    material.TotalPainting = material.Painting * material.Quantity;
                    material.TotalSupport = material.Supporting * material.Quantity;
                    material.TotalAccessory = material.Accessory * material.Quantity;
                    material.TotalFitting = material.Fittings * material.Quantity;

                    material.TotalNetCost = material.TotalNetPrice + material.Installation + material.TotalAccessory + material.TotalFitting + material.TotalPainting + material.TotalSupport;
                    material.TotalCost = material.TotalOfferPrice + material.Installation + material.TotalAccessory + material.TotalFitting + material.TotalPainting + material.TotalSupport;
                }
            }
            return newProjectMaterialGroup;
        }
    }
}
