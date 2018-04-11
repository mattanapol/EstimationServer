using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
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
        private readonly IProjectMaterialRepository _materialRepository;

        /// <summary>
        /// Constructor of project summary service
        /// </summary>
        /// <param name="projectMaterialGroupService"></param>
        /// <param name="projectService"></param>
        /// <param name="materialRepository"></param>
        public ProjectSummaryService(IProjectMaterialGroupService projectMaterialGroupService, IProjectService projectService, IProjectMaterialRepository materialRepository) 
        {
            _projectMaterialGroupService = projectMaterialGroupService ?? throw new ArgumentNullException(nameof(projectMaterialGroupService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        }

        /// <summary>
        /// Adjust summary of provided group id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupSummaryIncomingDto"></param>
        /// <returns></returns>
        public async Task<GroupSummary> AdjustGroupSummary(int id, GroupSummaryIncomingDto groupSummaryIncomingDto)
        {
            var originalGroupSummary = await GetGroupSummary(id);

            var summaryRatio = new AdjustSummaryRatioDto(originalGroupSummary, groupSummaryIncomingDto);
            return await AdjustGroupSummary(id, groupSummaryIncomingDto, summaryRatio);
        }

        /// <summary>
        /// Adjust group summary by giving ratio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupSummaryIncomingDto"></param>
        /// <param name="summaryRatio"></param>
        /// <returns></returns>
        public async Task<GroupSummary> AdjustGroupSummary(int id, GroupSummaryIncomingDto groupSummaryIncomingDto, AdjustSummaryRatioDto summaryRatio)
        {
            var projectMaterialGroup = await _projectMaterialGroupService.GetProjectMaterialGroup(id);
            projectMaterialGroup.Miscellaneous = groupSummaryIncomingDto.MiscellaneousInfo;
            projectMaterialGroup.Transportation = groupSummaryIncomingDto.TransportationInfo;
            await _projectMaterialGroupService.UpdateProjectMaterialGroup(projectMaterialGroup.Id,projectMaterialGroup);
            GroupSummary groupSummary = new GroupSummary { MiscellaneousInfo = groupSummaryIncomingDto.MiscellaneousInfo, TransportationInfo = groupSummaryIncomingDto.TransportationInfo };
            if (projectMaterialGroup.ChildGroups.Count != 0)
            {
                int allMaterialQuantity = projectMaterialGroup.GetMaterialsQuantity();
                // Sum all groups
                foreach (var group in projectMaterialGroup.ChildGroups)
                {
                    int groupMaterialQuantity = group.GetMaterialsQuantity();
                    var childGroupSummary = await AdjustGroupSummary(id, groupSummaryIncomingDto.Split((decimal)groupMaterialQuantity/allMaterialQuantity), summaryRatio);
                    groupSummary.AddByGroupSummary(childGroupSummary);
                }
            }
            else if (projectMaterialGroup.Materials.Count != 0)
            {
                // Sum all materials
                foreach (var material in projectMaterialGroup.Materials) //ToDo: Handle when original material is 0
                {
                    if (material.Quantity == 0)
                        continue;

                    if (summaryRatio.Accessories != -1)
                        material.Accessory = material.Accessory * summaryRatio.Accessories;
                    else
                        material.Accessory = (decimal)groupSummaryIncomingDto.Accessories / (decimal)material.Quantity;

                    if (summaryRatio.Fittings != -1)
                        material.Fittings = material.Fittings * summaryRatio.Fittings;
                    else
                        material.Fittings = (decimal)groupSummaryIncomingDto.Fittings / (decimal)material.Quantity;

                    if (summaryRatio.Painting != -1)
                        material.Painting = material.Painting * summaryRatio.Painting;
                    else
                        material.Painting = (decimal)groupSummaryIncomingDto.Painting / (decimal)material.Quantity;

                    if (summaryRatio.Supporting != -1)
                        material.Supporting = material.Supporting * summaryRatio.Supporting;
                    else
                        material.Supporting = (decimal)groupSummaryIncomingDto.Supporting / (decimal)material.Quantity;

                    if (summaryRatio.Installation != -1)
                        material.Manpower = material.Manpower * summaryRatio.Installation;
                    else
                        material.Manpower = (decimal)groupSummaryIncomingDto.Installation / (material.Quantity * projectMaterialGroup.ProjectInfo.LabourCost);

                    await _materialRepository.UpdateMaterial(material.Id, material);
                }

                groupSummary = await GetGroupSummary(id);

            }

            return groupSummary;
        }

        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GroupSummary> GetGroupSummary(int id)
        {
            var projectMaterialGroup = await _projectMaterialGroupService.GetProjectMaterialGroup(id);

            GroupSummary groupSummary = await GetGroupSummary(projectMaterialGroup);

            return groupSummary;
        }

        /// <summary>
        /// Get group summary by project material group
        /// </summary>
        /// <param name="projectMaterialGroup"></param>
        /// <returns></returns>
        public async Task<GroupSummary> GetGroupSummary(ProjectMaterialGroup projectMaterialGroup)
        {
            GroupSummary groupSummary = new GroupSummary { MiscellaneousInfo = projectMaterialGroup.Miscellaneous, TransportationInfo = projectMaterialGroup.Transportation };
            if (projectMaterialGroup.ChildGroups.Count != 0)
            {
                // Sum all groups
                foreach (var group in projectMaterialGroup.ChildGroups)
                {
                    var childGroupSummary = await GetGroupSummary(group);
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
