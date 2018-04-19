using Estimation.Domain.Models;
using Estimation.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Services
{
    public class ExportService : IExportService
    {
        public Task<byte[]> ExportProjectToPdf(int id, ProjectExportRequest exportRequest)
        {
            throw new NotImplementedException();
        }
    }
}
