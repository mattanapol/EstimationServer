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

        public ProjectSummaryService(IProjectMaterialGroupService projectMaterialGroupService)
        {
            _projectMaterialGroupService = projectMaterialGroupService ?? throw new ArgumentNullException(nameof(projectMaterialGroupService));
        }

        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GroupSummary> GetGroupSummary(int id)
        {
            var projectMaterialGroup = await _projectMaterialGroupService.GetProjectMaterialGroup(id);

            GroupSummary groupSummary = new GroupSummary { MiscellaneousInfo = projectMaterialGroup.Miscellaneous, TransportationInfo = projectMaterialGroup.Transportation };
            if (projectMaterialGroup.ChildGroups.Count != 0)
            {
                // Sum all groups

            }
            else if (projectMaterialGroup.Materials.Count != 0)
            {
                // Sum all materials
                foreach (var material in projectMaterialGroup.Materials)
                {
                    groupSummary.Accessories += material.Accessory;
                    groupSummary.Fittings += material.Fittings;
                    groupSummary.Painting += material.Painting;
                    groupSummary.Supporting += material.Supporting;
                    //groupSummary.Installation += material.
                }
            }

            return groupSummary;
        }

        /// <summary>
        /// Get project summary by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ProjectSummary> GetProjectSummary(int id)
        {
            throw new NotImplementedException();
        }
    }
}
