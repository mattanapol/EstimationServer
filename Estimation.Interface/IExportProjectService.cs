using System.Threading.Tasks;
using Estimation.Domain.Models;

namespace Estimation.Interface
{
    public interface IExportProjectService
    {
        /// <summary>
        /// Exports the project estimation.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="printOrder">The print order.</param>
        /// <returns></returns>
        Task<byte[]> ExportProjectEstimation(int projectId, ProjectExportRequest printOrder);
    }
}