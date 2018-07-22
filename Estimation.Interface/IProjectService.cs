using Estimation.Domain.Models;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IProjectService
    {
        /// <summary>
        /// Get project information by project id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<ProjectInfo> GetProject(int projectId);

        /// <summary>
        /// Clones the specified project identifier.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        Task<ProjectInfo> Clone(int projectId);
    }
}
