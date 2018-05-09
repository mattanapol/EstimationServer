using Kaewsai.HtmlToPdf.Domain;

namespace Estimation.Domain.Models
{
    public class PrintOrderRequest
    {
        /// <summary>
        /// Is portrait
        /// </summary>
        public bool IsPortrait { get; set; } = true;
        
        /// <summary>
        /// Paper kind
        /// </summary>
        public PaperKind Paper { get; set; } = PaperKind.A4;
    }
}
