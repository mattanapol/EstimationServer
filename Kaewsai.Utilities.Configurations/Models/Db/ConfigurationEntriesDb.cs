using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kaewsai.Utilities.Configurations.Models.Db
{
    [Table("ConfigurationEntries")]
    public class ConfigurationEntriesDb
    {
        /// <summary>
        /// Gets or sets the configuration entry identifier.
        /// </summary>
        /// <value>The configuration entry identifier.</value>
        [Key]
        public int ConfigurationEntryId { get; set; }

        /// <summary>
        /// Gets or sets the configuration dict title.(ForeignKey)
        /// </summary>
        /// <value>The configuration dict title.</value>
        [Required]
        public string ConfigurationDictTitle { get; set; }

        /// <summary>
        /// Gets or sets the configuration dict this entry belong to.
        /// </summary>
        /// <value>The configuration dict.</value>
        public ConfigurationDictDb ConfigurationDict { get; set; }

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
        /// Gets or sets the English description.
        /// </summary>
        /// <value>The english description.</value>
        public string EnglishDescription { get; set; }

        /// <summary>
        /// Gets or sets the Thai description.
        /// </summary>
        /// <value>The thai description.</value>
        public string ThaiDescription { get; set; }
    }
}
