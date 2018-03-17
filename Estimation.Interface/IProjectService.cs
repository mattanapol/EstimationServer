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
    }
}
