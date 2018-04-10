using Estimation.Domain.Dtos;
using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IProjectSummaryService
    {
        /// <summary>
        /// Get project summary by project id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProjectSummary> GetProjectSummary(int id);

        /// <summary>
        /// Get group summary by group id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GroupSummary> GetGroupSummary(int id);

        /// <summary>
        /// Adjust group summary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupSummaryIncomingDto"></param>
        /// <returns></returns>
        Task<GroupSummary> AdjustGroupSummary(int id, GroupSummaryIncomingDto groupSummaryIncomingDto);
    }
}
