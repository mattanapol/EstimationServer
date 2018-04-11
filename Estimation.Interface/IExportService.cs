using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IExportService
    {
        Task<byte[]> GetExportedPdf(ExportRequest exportRequest);
    }
}
