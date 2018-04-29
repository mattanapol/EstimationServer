using Kaewsai.HtmlToPdf.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
