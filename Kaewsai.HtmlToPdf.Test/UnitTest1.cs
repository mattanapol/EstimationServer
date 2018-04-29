using System;
using System.IO;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kaewsai.HtmlToPdf.Domain;
using Kaewsai.HtmlToPdf.Interface;
using Xunit;

namespace Kaewsai.HtmlToPdf.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            IConverter pdfConverter = new SynchronizedConverter(new PdfTools());
            IPdfGeneratorService pdfGenerator = new PdfGeneratorService(pdfConverter);

            // Get test data
            var testContent = GetTestPdfGeneratorInputContent();
            byte[] generatedPdf = await pdfGenerator.GetPdfFromHtmlAsync(testContent);

            await File.WriteAllBytesAsync("resultTest1.pdf",generatedPdf);
        }

        private PdfGeneratorInputContent GetTestPdfGeneratorInputContent()
        {
            var htmlContents = new PdfGeneratorInputContent();

            htmlContents.DPI = 150;
            htmlContents.Html.Add(File.ReadAllText("TestHtml.html"));

            return htmlContents;
        }
    }
}