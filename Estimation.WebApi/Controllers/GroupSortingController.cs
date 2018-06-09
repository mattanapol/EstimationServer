using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Dtos;
using Estimation.Interface;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estimation.WebApi.Controllers
{
    /// <summary>
    /// For sorting group and sub-group
    /// </summary>
    [Produces("application/json")]
    [Route("api/project/")]
    public class GroupSortingController : Controller
    {
        private readonly IGroupSortingService _groupSortingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupSortingController"/> class.
        /// </summary>
        /// <param name="groupSortingService">The group sorting service.</param>
        public GroupSortingController(IGroupSortingService groupSortingService)
        {
            _groupSortingService = groupSortingService ?? throw new ArgumentNullException(nameof(groupSortingService));
        }

        /// <summary>
        /// Sort group in specific project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="sortedGroupId">Sorted group Id</param>
        [HttpPut("group/{projectId}/sort")]
        public async Task<IActionResult> SortMaterialGroup(int projectId, [FromBody]GroupSortingDto sortedGroupId)
        {
            await _groupSortingService.SortGroupByProjectId(projectId, sortedGroupId.GroupIds);
            return Ok(OutgoingResult<string>.SuccessResponse("Sorted"));
        }

        /// <summary>
        /// Sort sub-group in specific project
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <param name="sortedGroupId">Sorted sub-group Id</param>
        [HttpPut("subgroup/{groupId}/sort")]
        public async Task<IActionResult> SortMaterialSubGroup(int groupId, [FromBody]GroupSortingDto sortedGroupId)
        {
            await _groupSortingService.SortSubGroupByGroupId(groupId, sortedGroupId.GroupIds);
            return Ok(OutgoingResult<string>.SuccessResponse("Sorted"));
        }
    }
}