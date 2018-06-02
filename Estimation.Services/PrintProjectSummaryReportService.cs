using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Services.Helpers;
using HtmlAgilityPack;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;

namespace Estimation.Services
{
    public class PrintProjectSummaryReportService : IPrintProjectSummaryReportService
    {
        private const string FormPath = "Forms/SummaryOfEstimation.html";
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IProjectSummaryService _projectSummaryService;

        public PrintProjectSummaryReportService(IPdfGeneratorService pdfGeneratorService, IProjectSummaryService projectSummaryService)
        {
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
        }

        public async Task<byte[]> GetProjectSummaryAsPdf(int projectId, ProjectExportRequest printOrder)
        {
            var projectDetails = await _projectSummaryService.GetProjectSummary(projectId);
            var htmlTemplate = File.ReadAllText(FormPath);

            var html = new HtmlDocument();
            html.LoadHtml(htmlTemplate);
            var root = html.DocumentNode;

            HtmlParser.ParseHtmlNodeByClass(root, projectDetails);

            // ------Get Pdf from html
            PdfGeneratorInputContent pdfContents = new PdfGeneratorInputContent()
            {
                Html = { html.DocumentNode.OuterHtml },
                Portrait = printOrder.IsPortrait,
                PaperKind = printOrder.Paper
            };
            var result = await _pdfGeneratorService.GetPdfFromHtmlAsync(pdfContents);
            return result;
        }
    }
}
