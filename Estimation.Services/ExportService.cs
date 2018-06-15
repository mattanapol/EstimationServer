using Estimation.Domain.Models;
using Estimation.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;

namespace Estimation.Services
{
    public class ExportService : IExportService
    {
        private readonly IPdfGeneratorService _pdfGeneratorService;

        public ExportService(IPdfGeneratorService pdfGeneratorService)
        {
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
        }

        /// <summary>
        /// Export project to pdf file
        /// </summary>
        /// <param name="htmls"></param>
        /// <param name="exportRequest"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportProjectToPdf(IEnumerable<string> htmls, ProjectExportRequest exportRequest)
        {
            PdfGeneratorInputContent pdfContents = new PdfGeneratorInputContent(htmls)
            {
                Portrait = exportRequest.IsPortrait,
                PaperKind = exportRequest.Paper
            };
            var result = await _pdfGeneratorService.GetPdfFromHtmlAsync(pdfContents);
            return result;
        }
    }
}
