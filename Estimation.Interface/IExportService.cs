using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IExportService
    {
        /// <summary>
        /// Export project to pdf file
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="exportRequest"></param>
        /// <returns></returns>
        Task<byte[]> ExportProjectToPdf(int id,ProjectExportRequest exportRequest);
    }
}
