using System.Threading.Tasks;
using Estimation.Domain.Models;

namespace Estimation.Interface
{
    public interface IPrintProjectSummaryReportService
    {
        Task<byte[]> GetProjectSummaryAsPdf(int projectId, ProjectExportRequest printOrder);
    }
}