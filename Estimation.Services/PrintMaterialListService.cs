using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface.Repositories;
using Estimation.Services.Helpers;
using HtmlAgilityPack;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;

namespace Estimation.Services
{
    public class PrintMaterialListService : IPrintMaterialListService
    {
        private const string FormPath = "Forms/MaterialList.html";
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IMaterialRepository _materialRepository;


        public PrintMaterialListService(IPdfGeneratorService pdfGeneratorService, IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository ?? throw  new ArgumentNullException(nameof(materialRepository));
            _pdfGeneratorService = pdfGeneratorService ?? throw new ArgumentNullException(nameof(pdfGeneratorService));
        }

        public async Task<byte[]> GetMaterialListAsPdf(PrintOrderRequest printOrder)
        {
            var mainMaterials = await _materialRepository.GetMaterialListWithFullInfo("");
            // Load form path from config
            var htmlTemplate = File.ReadAllText(FormPath);

            var html = new HtmlDocument();
            html.LoadHtml(htmlTemplate);
            var root = html.DocumentNode;
            // -----Processing Content in table
            var rowTemplate = root.Descendants()
                                  .First(n => n.GetAttributeValue("class", "").Equals("row-content"));
            var rowParent = rowTemplate.ParentNode;
            rowParent.RemoveChild(rowTemplate);
            foreach (var mainMaterial in mainMaterials)
            {
                var mainMaterialRow = rowTemplate.Clone();
                mainMaterialRow.InnerHtml = HtmlParser.ParseHtml(mainMaterialRow.InnerHtml, mainMaterial.GetDataDictionary());
                rowParent.AppendChild(mainMaterialRow);
                foreach (var subMaterial in mainMaterial.SubMaterials)
                {
                    var subMaterialRow = rowTemplate.Clone();
                    subMaterialRow.InnerHtml = HtmlParser.ParseHtml(subMaterialRow.InnerHtml, subMaterial.GetDataDictionary());
                    rowParent.AppendChild(subMaterialRow);
                    foreach (var material in subMaterial.Materials)
                    {
                        var materialRow = rowTemplate.Clone();
                        materialRow.InnerHtml = HtmlParser.ParseHtml(materialRow.InnerHtml, material.GetDataDictionary());
                        rowParent.AppendChild(materialRow);
                    }
                }
            }

            
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
