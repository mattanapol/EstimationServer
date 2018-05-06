using Kaewsai.HtmlToPdf.Domain;

namespace Estimation.Domain.Models
{
    public class PrintOrderRequest
    {
        /// <summary>
        /// Is portait
        /// </summary>
        public bool IsPortait { get; set; } = true;
        
        /// <summary>
        /// Paper kind
        /// </summary>
        public PaperKind Paper { get; set; } = PaperKind.A4;
    }
}
