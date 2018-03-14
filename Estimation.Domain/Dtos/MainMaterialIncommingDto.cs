﻿using Estimation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    /// <summary>
    /// Main material incomming dto
    /// </summary>
    public class MainMaterialIncommingDto
    {
        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Material class
        /// </summary>
        public MaterialClass Class { get; set; }
    }
}
