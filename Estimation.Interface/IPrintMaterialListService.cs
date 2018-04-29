using System.Collections.Generic;
using System.Threading.Tasks;
using Estimation.Domain.Models;

namespace Estimation.Services
{
    public interface IPrintMaterialListService
    {
        Task<byte[]> GetMaterialListAsPdf(PrintOrderRequest printOrder);
    }
}