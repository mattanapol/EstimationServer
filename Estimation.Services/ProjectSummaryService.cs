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
                    groupSummary.Accessories += material.TotalAccessory;
                    groupSummary.Fittings += material.Totalfitting;
                    groupSummary.Painting += material.TotalPainting;
                    groupSummary.Supporting += material.TotalSupport;
                    groupSummary.Installation += material.Installation;
                    groupSummary.MaterialPrice += material.TotalOfferPrice;
                }

                groupSummary.Transportation = groupSummary.TransportationInfo.IsUsePercentage ?
                    groupSummary.TransportationInfo.Percentage * groupSummary.Installation / 100 :
                    groupSummary.TransportationInfo.Manual;

                groupSummary.Miscellaneous = groupSummary.MiscellaneousInfo.IsUsePercentage ?
                    groupSummary.MiscellaneousInfo.Percentage : //ToDo: percent of what?
                    groupSummary.MiscellaneousInfo.Manual;

                groupSummary.GrandTotal = Math.Ceiling((groupSummary.Accessories + groupSummary.Fittings 
                    + groupSummary.Painting + groupSummary.Supporting + groupSummary.Installation + groupSummary.MaterialPrice 
                    + groupSummary.Transportation + groupSummary.Miscellaneous) / (decimal)Math.Pow(10, projectMaterialGroup.ProjectInfo.CeilingSummary)) 
                    * (decimal)Math.Pow(10, projectMaterialGroup.ProjectInfo.CeilingSummary);
                
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
