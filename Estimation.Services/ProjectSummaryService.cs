using Estimation.Domain.Models;
using Estimation.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class ProjectSummaryService : IProjectSummaryService
    {
        private readonly IProjectMaterialGroupService _projectMaterialGroupService;
        private readonly IProjectService _projectService;

        public ProjectSummaryService(IProjectMaterialGroupService projectMaterialGroupService, IProjectService projectService)
        {
            _projectMaterialGroupService = projectMaterialGroupService ?? throw new ArgumentNullException(nameof(projectMaterialGroupService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GroupSummary> GetGroupSummary(int id)
        {
            var projectMaterialGroup = await _projectMaterialGroupService.GetProjectMaterialGroup(id);

            GroupSummary groupSummary = GetGroupSummary(projectMaterialGroup);

            return groupSummary;
        }

        /// <summary>
        /// Get group summary by project material group
        /// </summary>
        /// <param name="projectMaterialGroup"></param>
        /// <returns></returns>
        public GroupSummary GetGroupSummary(ProjectMaterialGroup projectMaterialGroup)
        {
            GroupSummary groupSummary = new GroupSummary { MiscellaneousInfo = projectMaterialGroup.Miscellaneous, TransportationInfo = projectMaterialGroup.Transportation };
            if (projectMaterialGroup.ChildGroups.Count != 0)
            {
                // Sum all groups
                foreach (var group in projectMaterialGroup.ChildGroups)
                {
                    var childGroupSummary = GetGroupSummary(group);
                    groupSummary.AddByGroupSummary(childGroupSummary);
                }
            }
            else if (projectMaterialGroup.Materials.Count != 0)
            {
                // Sum all materials
                foreach (var material in projectMaterialGroup.Materials)
                {
                    groupSummary.AddByMaterial(material);
                }

                groupSummary.CalculateGrandTotal(projectMaterialGroup.ProjectInfo.CeilingSummary);

            }

            return groupSummary;
        }

        /// <summary>
        /// Get project summary by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProjectSummary> GetProjectSummary(int id)
        {
            var project = await _projectService.GetProject(id);
            var materialGroups = project.MaterialGroups;

            ProjectSummary projectSummary = new ProjectSummary();
            foreach(var materialGroup in materialGroups)
            {
                projectSummary.AddByGroupSummary(await GetGroupSummary(materialGroup.Id));
            }

            return projectSummary;
        }
    }
}
