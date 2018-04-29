using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estimation.Domain.Models;
using Estimation.Interface;
using Estimation.Interface.Repositories;
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
                mainMaterialRow.InnerHtml = mainMaterialRow.InnerHtml.Replace("##CODE##", mainMaterial.CodeAsString, StringComparison.Ordinal);
                mainMaterialRow.InnerHtml = mainMaterialRow.InnerHtml.Replace("##NAME##", mainMaterial.Name, StringComparison.Ordinal);
                mainMaterialRow.InnerHtml = mainMaterialRow.InnerHtml.Replace("##DESCRIPTION##", mainMaterial.Description, StringComparison.Ordinal);
                rowParent.AppendChild(mainMaterialRow);
                foreach (var subMaterial in mainMaterial.SubMaterials)
                {
                    var subMaterialRow = rowTemplate.Clone();
                    subMaterialRow.InnerHtml = subMaterialRow.InnerHtml.Replace("##CODE##", subMaterial.CodeAsString, StringComparison.Ordinal);
                    subMaterialRow.InnerHtml = subMaterialRow.InnerHtml.Replace("##NAME##", subMaterial.Name, StringComparison.Ordinal);
                    subMaterialRow.InnerHtml = subMaterialRow.InnerHtml.Replace("##DESCRIPTION##", subMaterial.Description, StringComparison.Ordinal);
                    rowParent.AppendChild(subMaterialRow);
                    foreach (var material in subMaterial.Materials)
                    {
                        var row = rowTemplate.Clone();
                        row.InnerHtml = row.InnerHtml.Replace("##CODE##", material.CodeAsString, StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##NAME##", material.Name, StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##DESCRIPTION##", material.Description, StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##LISTPRICE##", material.ListPrice.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##NETPRICE##", material.NetPrice.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##OFFERPRICE##", material.OfferPrice.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##MANPOWER##", material.Manpower.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##FITTINGS##", material.Fittings.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##ACCESSORY##", material.Accessory.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##SUPPORTING##", material.Supporting.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##PAINTING##", material.Painting.ToString(), StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##REMARK##", material.Remark, StringComparison.Ordinal);
                        row.InnerHtml = row.InnerHtml.Replace("##UNIT##", material.Unit, StringComparison.Ordinal);
                        rowParent.AppendChild(row);
                    }
                }
            }

            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##LISTPRICE##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##NETPRICE##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##OFFERPRICE##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##MANPOWER##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##FITTINGS##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##ACCESSORY##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##SUPPORTING##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##PAINTING##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##REMARK##", "-", StringComparison.Ordinal);
            html.DocumentNode.InnerHtml = html.DocumentNode.InnerHtml.Replace("##UNIT##", "-", StringComparison.Ordinal);

            // ------Get Pdf from html
            PdfGeneratorInputContent pdfContents = new PdfGeneratorInputContent()
            {
                Html = { html.DocumentNode.OuterHtml },
                Portrait = printOrder.IsPortait,
                PaperKind = printOrder.Paper
            };
            var result = await _pdfGeneratorService.GetPdfFromHtmlAsync(pdfContents);
            return result;
        }
    }
}
