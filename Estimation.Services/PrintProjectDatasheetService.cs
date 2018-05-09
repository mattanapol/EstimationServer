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
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSummaryService _projectSummaryService;


        public PrintProjectDatasheetService(IPdfGeneratorService pdfGeneratorService, IProjectRepository projectRepository, IProjectSummaryService projectSummaryService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
            _projectSummaryService = projectSummaryService ?? throw new ArgumentNullException(nameof(projectSummaryService));
        }

        public async Task<byte[]> GetProjectDatasheetAsPdf(int projectId, PrintOrderRequest printOrder)
        {
            var projectDetails = await _projectSummaryService.GetProjectSummary(projectId);
            // Load form path from config
            var htmlTemplate = File.ReadAllText(FormPath);

            var html = new HtmlDocument();
            html.LoadHtml(htmlTemplate);
            var root = html.DocumentNode;

            HtmlParser.ParseHtmlNodeByClass(root, "row-content", projectDetails.ChildSummaries);
            //// -----Processing Content in table
            //var rowTemplate = root.Descendants()
            //                      .First(n => n.GetAttributeValue("class", "").Equals("row-content"));
            //var rowParent = rowTemplate.ParentNode;
            //rowParent.RemoveChild(rowTemplate);
            //foreach (var groupSummary in projectDetails.ChildSummaries)
            //{
            //    var mainMaterialRow = rowTemplate.Clone();
            //    mainMaterialRow.InnerHtml = HtmlParser.ParseHtml(mainMaterialRow.InnerHtml, materialGroup.GetDataDictionary());
            //    rowParent.AppendChild(mainMaterialRow);
            //    foreach (var subMaterial in mainMaterial.SubMaterials)
            //    {
            //        var subMaterialRow = rowTemplate.Clone();
            //        subMaterialRow.InnerHtml = HtmlParser.ParseHtml(subMaterialRow.InnerHtml, subMaterial.GetDataDictionary());
            //        rowParent.AppendChild(subMaterialRow);
            //        foreach (var material in subMaterial.Materials)
            //        {
            //            var materialRow = rowTemplate.Clone();
            //            materialRow.InnerHtml = HtmlParser.ParseHtml(materialRow.InnerHtml, material.GetDataDictionary());
            //            rowParent.AppendChild(materialRow);
            //        }
            //    }
            //}


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
