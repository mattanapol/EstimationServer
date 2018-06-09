using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    /// <summary>
    /// Interface for group sorting service
    /// </summary>
    public interface IGroupSortingService
    {
        /// <summary>
        /// Sorts the group by project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="groupIds">The group ids.</param>
        /// <returns></returns>
        Task SortGroupByProjectId(int projectId, IEnumerable<int> groupIds);
        /// <summary>
        /// Sorts the sub group by group identifier.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="subGroupIds">The sub group ids.</param>
        /// <returns></returns>
        Task SortSubGroupByGroupId(int groupId, IEnumerable<int> subGroupIds);
    }
}
