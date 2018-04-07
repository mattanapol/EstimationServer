using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.HtmlToPdf.Domain
{
    public class PdfGeneratorInputContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kaewsai.HtmlToPdf.Domain.PdfGeneratorInputContent"/> class.
        /// </summary>
        public PdfGeneratorInputContent()
        {
            Html = new List<string>();
        }

        /// <summary>
        /// Gets or sets the html.
        /// </summary>
        /// <value>The html.</value>
        public List<string> Html
        {
            get;
        }

        /// <summary>
        /// Gets or sets the dpi of file.
        /// </summary>
        /// <value>The dpi.</value>
        public int DPI { get; set; } = 300;
        /// <summary>
        /// Gets or sets the portrait of paper.
        /// </summary>
        /// <value>The portrait.</value>
        public bool Portrait { get; set; } = true;

        /// <summary>
        /// Gets or sets the width of paper.
        /// </summary>
        /// <value>The width.</value>
        public int Width { get; set; } = -1;

        /// <summary>
        /// Gets or sets the height of paper.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; set; } = -1;

        /// <summary>
        /// Gets or sets the kind of the paper.
        /// </summary>
        /// <value>The kind of the paper.</value>
        public PaperKind PaperKind { get; set; }

        /// <summary>
        /// Get DinkToPdf file
        /// </summary>
        /// <returns></returns>
        public DinkToPdf.PaperKind GetDinkToPdfPaperKind()
        {
            switch (this.PaperKind)
            {
                case Domain.PaperKind.A2:
                    return DinkToPdf.PaperKind.A2;
                case Domain.PaperKind.A3:
                    return DinkToPdf.PaperKind.A3;
                case Domain.PaperKind.A4:
                    return DinkToPdf.PaperKind.A4;
                case Domain.PaperKind.A5:
                    return DinkToPdf.PaperKind.A5;
                case Domain.PaperKind.A6:
                    return DinkToPdf.PaperKind.A6;
                case Domain.PaperKind.B4:
                    return DinkToPdf.PaperKind.B4;
                case Domain.PaperKind.B5:
                    return DinkToPdf.PaperKind.B5;
                case Domain.PaperKind.Letter:
                    return DinkToPdf.PaperKind.Letter;
                case Domain.PaperKind.Custom:
                    return DinkToPdf.PaperKind.Custom;
                default:
                    return DinkToPdf.PaperKind.A4;
            }
        }
    }
}
