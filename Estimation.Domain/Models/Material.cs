using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class Material
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal ListPrice { get; set; }

        public decimal NetPrice { get; set; }

        public decimal OfferPrice { get; set; }

        public decimal Manpower { get; set; }

        public decimal Fittings { get; set; }

        public decimal Supporting { get; set; }

        public decimal Painting { get; set; }
    }
}
