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
                var projectMaterialGroupChilds = projectMaterialGroups.Where(e => e.ParentGroupId.GetValueOrDefault(0) == result.Id);
                result.ChildGroups = new Collection<ProjectMaterialGroup>(projectMaterialGroupChilds.ToList());
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
            var projectMaterialGroups = (await _projectMaterialGroupRepository.GetAllProjectMaterialGroupInProject(projectMaterialGroup.ProjectId))
                .Where(e => e.ParentGroupId.GetValueOrDefault(0) == id).Select(e => { e.ProjectInfo = projectInfo; return e; });
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

                    material.TotalAccessory = material.Accessory * material.Quantity;

                    material.Totalfitting = material.Fittings * material.Quantity;

                    material.TotalOfferPrice = material.OfferPrice * material.Quantity;

                    material.TotalPainting = material.Painting * material.Quantity;

                    material.TotalSupport = material.Supporting * material.Quantity;

                    material.TotalCost = material.Installation + material.TotalAccessory + material.Totalfitting + material.TotalOfferPrice + material.TotalPainting + material.TotalSupport;
                }
            }
            return newProjectMaterialGroup;
        }
    }
}
