using System.Threading.Tasks;
using Estimation.Domain.Models;

namespace Estimation.Interface
{
    public interface IPrintProjectDatasheetService
    {
        Task<byte[]> GetProjectDatasheetAsPdf(int projectId, PrintOrderRequest printOrder);
    }
}