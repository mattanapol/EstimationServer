using System.Threading.Tasks;
using Estimation.Domain.Models;

namespace Estimation.Interface
{
    public interface IPrintProjectSummaryReportService
    {
        /// <summary>
        /// Gets the project summary as PDF.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="printOrder">The print order.</param>
        /// <returns></returns>
        Task<byte[]> GetProjectSummaryAsPdf(int projectId, ProjectExportRequest printOrder);

        /// <summary>
        /// Gets the project summary as HTML.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="printOrder"></param>
        /// <returns></returns>
        Task<string> GetProjectSummaryAsHtml(int projectId, ProjectExportRequest printOrder);

    }
}