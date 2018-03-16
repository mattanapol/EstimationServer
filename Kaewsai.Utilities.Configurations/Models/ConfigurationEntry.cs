using System;
using System.Collections.Generic;
using System.Text;

namespace Kaewsai.Utilities.Configurations.Models
{
    public class ConfigurationEntry
    {
        /// <summary>
        /// Gets or sets the configuration entry identifier.
        /// </summary>
        /// <value>The configuration entry identifier.</value>
        public int ConfigurationEntryId { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the internal description.
        /// </summary>
        /// <value>The internal description.</value>
        public string InternalDescription { get; set; }

        /// <summary>
        /// Gets or sets the english description.
        /// </summary>
        /// <value>The english description.</value>
        public string EnglishDescription { get; set; }

        /// <summary>
        /// Gets or sets the thai description.
        /// </summary>
        /// <value>The thai description.</value>
        public string ThaiDescription { get; set; }
    }
}
