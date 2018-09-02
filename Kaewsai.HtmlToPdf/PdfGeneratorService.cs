using DinkToPdf;
using DinkToPdf.Contracts;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.HtmlToPdf
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        private readonly IConverter _pdfConverter;

        /// <summary>
        /// Pdf generator service constructor
        /// </summary>
        /// <param name="pdfConverter"></param>
        public PdfGeneratorService(IConverter pdfConverter)
        {
            _pdfConverter = pdfConverter ?? throw new ArgumentNullException(nameof(pdfConverter));
        }

        /// <summary>
        /// Get pdf from html
        /// </summary>
        /// <param name="inputContent"></param>
        /// <returns></returns>
        public async Task<byte[]> GetPdfFromHtmlAsync(PdfGeneratorInputContent inputContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = inputContent.Portrait ? Orientation.Portrait : Orientation.Landscape,
                    DPI = inputContent.DPI,
                    PaperSize = inputContent.GetDinkToPdfPaperKind()
                }
            };

            if (inputContent.GetDinkToPdfPaperKind() == DinkToPdf.PaperKind.Custom)
            {
                doc.GlobalSettings.PaperSize =
                    new PechkinPaperSize(inputContent.Width.ToString(), inputContent.Height.ToString());
            }
            foreach (string html in inputContent.Html)
            {
                doc.Objects.Add(new ObjectSettings()
                {
                    PagesCount = true,
                    HtmlContent = html,
                    WebSettings = {DefaultEncoding = "utf-8"},
                    FooterSettings = { FontSize = 8, Center = "Page [page] / [toPage]", Spacing = 2.812 }
                });
            }

            return await Task.Run(() => _pdfConverter.Convert(doc));
        }
    }
}