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
                    groupSummary.Accessories += childGroupSummary.Accessories;
                    groupSummary.Fittings += childGroupSummary.Fittings;
                    groupSummary.Painting += childGroupSummary.Painting;
                    groupSummary.Supporting += childGroupSummary.Supporting;
                    groupSummary.Installation += childGroupSummary.Installation;
                    groupSummary.MaterialPrice += childGroupSummary.MaterialPrice;
                    groupSummary.Transportation += childGroupSummary.Transportation;
                    groupSummary.Miscellaneous += childGroupSummary.Miscellaneous;
                }
            }
            else if (projectMaterialGroup.Materials.Count != 0)
            {
                // Sum all materials
                foreach (var material in projectMaterialGroup.Materials)
                {
                    groupSummary.Accessories += (int)Math.Round(material.TotalAccessory);
                    groupSummary.Fittings += (int)Math.Round(material.Totalfitting);
                    groupSummary.Painting += (int)Math.Round(material.TotalPainting);
                    groupSummary.Supporting += (int)Math.Round(material.TotalSupport);
                    groupSummary.Installation += (int)Math.Round(material.Installation);
                    groupSummary.MaterialPrice += (int)Math.Round(material.TotalOfferPrice);
                }

                groupSummary.Transportation = (int)Math.Round(groupSummary.TransportationInfo.IsUsePercentage ?
                    groupSummary.TransportationInfo.Percentage * groupSummary.Installation / 100 :
                    groupSummary.TransportationInfo.Manual);

                groupSummary.Miscellaneous = (int)Math.Round(groupSummary.MiscellaneousInfo.IsUsePercentage ?
                    groupSummary.MiscellaneousInfo.Percentage * groupSummary.MaterialPrice / 100 :
                    groupSummary.MiscellaneousInfo.Manual);

                int total = (groupSummary.Accessories + groupSummary.Fittings
                    + groupSummary.Painting + groupSummary.Supporting + groupSummary.Installation + groupSummary.MaterialPrice
                    + groupSummary.Transportation + groupSummary.Miscellaneous);

                int roundedTotal = (int)(Math.Ceiling((double)total / Math.Pow(10, projectMaterialGroup.ProjectInfo.CeilingSummary))
                    * Math.Pow(10, projectMaterialGroup.ProjectInfo.CeilingSummary));

                // Adjust Miscellaneous
                groupSummary.Miscellaneous += roundedTotal - total;

                groupSummary.GrandTotal = roundedTotal;

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
