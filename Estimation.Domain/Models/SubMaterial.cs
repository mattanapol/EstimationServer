﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class SubMaterial: MaterialInfo
    {
        /// <summary>
        /// Submaterial
        /// </summary>
        public IEnumerable<MaterialInfo> Materials { get; set; }
    }
}