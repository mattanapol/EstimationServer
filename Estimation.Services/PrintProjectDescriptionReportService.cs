using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Excel;
using Estimation.Interface;
using Estimation.Services.Helpers;
using HtmlAgilityPack;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;

namespace Estimation.Services
{
    public class PrintProjectDescriptionReportService: IPrintProjectDescriptionReportService
    {
        private const string SubmitFormPath = "Forms/DescriptionOfEstimation_SubmitForm.html";
        private const string DetailFormPath = "Forms/DescriptionOfEstimation_DetailForm.html";
        private const string NetFormPath = "Forms/DescriptionOfEstimation_NetForm.html";
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IProjectSummaryService _projectSummaryService;
        private readonly DescriptionOfEstimationForm _descriptionOfEstimationForm;

        public PrintProjectDescriptionReportService(IProjectSummaryService projectSummaryService, IPdfGeneratorService pdfGeneratorService)
        {
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
            _descriptionOfEstimationForm = new DescriptionOfEstimationForm();
        }

        public async Task<byte[]> GetProjectDescriptionAsPdf(int projectId, ProjectExportRequest printOrder)
        {
            string html = await GetProjectDescriptionAsHtml(projectId, printOrder);

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

        public async Task<string> GetProjectDescriptionAsHtml(int projectId, ProjectExportRequest printOrder)
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

        public async Task<byte[]> GetProjectDescriptionAsExcel(int projectId, ProjectExportRequest printOrder)
        {
            var projectDetails = await _projectSummaryService.GetProjectSummary(projectId);
            var excelFileAsByteArray = _descriptionOfEstimationForm.ExportToExcel(projectDetails, printOrder);
            return excelFileAsByteArray;
        }
    }
}
