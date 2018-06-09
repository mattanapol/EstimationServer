using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimation.Interface;
using Estimation.Interface.Repositories;

namespace Estimation.Services
{
    /// <summary>
    /// Group sorting service
    /// </summary>
    /// <seealso cref="Estimation.Interface.IGroupSortingService" />
    public class GroupSortingService: IGroupSortingService
    {
        private readonly IProjectMaterialGroupService _projectMaterialGroupService;

        public GroupSortingService(IProjectMaterialGroupService projectMaterialGroupService)
        {
            _projectMaterialGroupService = projectMaterialGroupService ?? throw new ArgumentNullException(nameof(projectMaterialGroupService));
        }

        /// <inheritdoc />
        public async Task SortGroupByProjectId(int projectId, IEnumerable<int> groupIds)
        {
            var materialGroups = await _projectMaterialGroupService.GetAllProjectMaterial(projectId);
            var groupIdArray = groupIds as int[] ?? groupIds.ToArray();
            for (var i = 0; i < groupIdArray.Count(); i++)
            {
                var materialGroup = materialGroups.SingleOrDefault(g => g.Id == groupIdArray[i]);
                if (materialGroup == null) throw new ArgumentOutOfRangeException($"Group id {groupIdArray[i]} is not exist in project id {projectId}");
                await _projectMaterialGroupService.UpdateProjectMaterialGroupOrder(groupIdArray[i], i);
            }
        }

        /// <inheritdoc />
        public async Task SortSubGroupByGroupId(int groupId, IEnumerable<int> subGroupIds)
        {
            var group = await _projectMaterialGroupService.GetProjectMaterialGroup(groupId);
            var groupIdArray = subGroupIds as int[] ?? subGroupIds.ToArray();
            for (var i = 0; i < groupIdArray.Count(); i++)
            {
                var materialGroup = group.ChildGroups.SingleOrDefault(g => g.Id == groupIdArray[i]);
                if (materialGroup == null) throw new ArgumentOutOfRangeException($"Group id {groupIdArray[i]} is not exist or not a child of group id {groupId}");
                await _projectMaterialGroupService.UpdateProjectMaterialSubGroupOrder(groupIdArray[i], group.Order, i);
            }
        }
    }
}
