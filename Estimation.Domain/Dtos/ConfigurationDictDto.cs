using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Dtos
{
    public class ConfigurationDictDto
    {
        /// <summary>
        /// Configuration Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Configuration configuration
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Configuration entries
        /// </summary>
        public IEnumerable<ConfigurationEntryDto> ConfigurationEntries { get; set; }
    }
}
