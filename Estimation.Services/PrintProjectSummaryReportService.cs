using System;
using System.IO;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Excel;
using Estimation.Interface;
using Estimation.Services.Helpers;
using HtmlAgilityPack;
using Kaewsai.Excel;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;

namespace Estimation.Services
{
    public class PrintProjectSummaryReportService : IPrintProjectSummaryReportService
    {
        private const string SubmitFormPath = "Forms/SummaryOfEstimation_SubmitForm.html";
        private const string DetailFormPath = "Forms/SummaryOfEstimation_DetailForm.html";
        private const string NetFormPath = "Forms/SummaryOfEstimation_NetForm.html";
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IProjectSummaryService _projectSummaryService;

        public PrintProjectSummaryReportService(IPdfGeneratorService pdfGeneratorService, IProjectSummaryService projectSummaryService)
        {
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
        }
        
        /// <inheritdoc />
        public async Task<byte[]> GetProjectSummaryAsPdf(int projectId, ProjectExportRequest printOrder)
        {
            string html = await GetProjectSummaryAsHtml(projectId, printOrder);

            // ------Get Pdf from html
            PdfGeneratorInputContent pdfContents = new PdfGeneratorInputContent()
            {
                Html = { html },
                Portrait = printOrder.IsPortrait,
                PaperKind = printOrder.Paper
            };
            var result = await _pdfGeneratorService.GetPdfFromHtmlAsync(pdfContents);
            return result;
        }

        /// <inheritdoc />
        public async Task<string> GetProjectSummaryAsHtml(int projectId, ProjectExportRequest printOrder)
        {
            var projectDetails = await _projectSummaryService.GetProjectSummary(projectId);
            string htmlTemplate;
            switch (printOrder.SubmitForm)
            {
                case SubmitForm.SubmitForm:
                    htmlTemplate = File.ReadAllText(SubmitFormPath);
                    break;
                case SubmitForm.MaterialAndLabourCostForm:
                    htmlTemplate = File.ReadAllText(DetailFormPath);
                    break;
                case SubmitForm.NetForm:
                    htmlTemplate = File.ReadAllText(NetFormPath);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var html = new HtmlDocument();
            html.LoadHtml(htmlTemplate);
            var root = html.DocumentNode;

            HtmlParser.ParseHtmlNodeByClass(root, projectDetails);

            return html.DocumentNode.OuterHtml;
        }
    }
}
