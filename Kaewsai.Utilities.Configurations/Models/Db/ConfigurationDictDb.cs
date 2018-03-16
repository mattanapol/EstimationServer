using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Kaewsai.Utilities.Configurations.Models.Db
{
    [Table("ConfigurationDict")]
    public class ConfigurationDictDb
    {
        /// <summary>
        /// Title of the configuration set
        /// </summary>
        [Key]
        public string Title { get; set; }

        /// <summary>
        /// Description of configuration set
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Configuration entries
        /// </summary>
        [ForeignKey("ConfigurationDictTitle")]
        public IEnumerable<ConfigurationEntriesDb> ConfigurationEntries { get; set; }
    }
}
