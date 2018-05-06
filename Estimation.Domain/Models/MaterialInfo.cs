﻿using System.Collections.Generic;

namespace Estimation.Domain.Models
{
    /// <summary>
    /// Material Information model
    /// </summary>
    public class MaterialInfo: IPrintable
    {
        /// <summary>
        /// Material Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Material Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Material code as string
        /// </summary>
        public string CodeAsString { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Material type
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Material class
        /// </summary>
        public MaterialClass Class { get; set; }

        /// <summary>
        /// Get data dictionary
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetDataDictionary()
        {
            var dataDict = new Dictionary<string, string>
            {
                {
                    "##CODE##", CodeAsString
                },
                {
                    "##NAME##", Name
                },
                {
                    "##DESCRIPTION##", Description
                },
                {
                    "##TYPE##", MaterialType
                },
                {
                    "##ID##", Id.ToString()
                }
            };

            return dataDict;
        }
    }
}
