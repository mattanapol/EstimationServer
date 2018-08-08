using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estimation.Interface
{
    public interface IPdfService
    {
        /// <summary>
        /// Export project to pdf file
        /// </summary>
        /// <param name="htmls"></param>
        /// <param name="exportRequest"></param>
        /// <returns></returns>
        Task<byte[]> ExportProjectToPdf(IEnumerable<string> htmls, PrintOrderRequest exportRequest);
    }
}
