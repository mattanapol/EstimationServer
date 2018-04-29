using Kaewsai.HtmlToPdf.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kaewsai.HtmlToPdf.Interface
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GetPdfFromHtmlAsync(PdfGeneratorInputContent inputContent);
    }
}
