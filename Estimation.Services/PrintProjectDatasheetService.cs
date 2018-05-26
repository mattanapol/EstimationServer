using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
using Estimation.Services.Helpers;
using HtmlAgilityPack;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;

namespace Estimation.Services
{
    /// <summary>
    /// Print project data sheet service class
    /// </summary>
    public class PrintProjectDatasheetService : IPrintProjectDatasheetService
    {
        private const string FormPath = "Forms/ProjectDatasheet.html";
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IProjectSummaryService _projectSummaryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintProjectDatasheetService"/> class.
        /// </summary>
        /// <param name="pdfGeneratorService">The PDF generator service.</param>
        /// <param name="projectSummaryService">The project summary service.</param>
        public PrintProjectDatasheetService(IPdfGeneratorService pdfGeneratorService, IProjectSummaryService projectSummaryService)
        {
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
        }

        /// <summary>
        /// Gets the project datasheet as PDF.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="printOrder">The print order.</param>
        /// <returns></returns>
        public async Task<byte[]> GetProjectDatasheetAsPdf(int projectId, PrintOrderRequest printOrder)
        {
            var projectDetails = await _projectSummaryService.GetProjectSummary(projectId);
            var htmlTemplate = File.ReadAllText(FormPath);

            var html = new HtmlDocument();
            html.LoadHtml(htmlTemplate);
            var root = html.DocumentNode;

            HtmlParser.ParseHtmlNodeByClass(root, projectDetails.ChildSummaries);

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
