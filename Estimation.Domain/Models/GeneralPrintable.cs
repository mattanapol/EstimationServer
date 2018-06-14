using System;
using System.Collections.Generic;
using System.Text;

namespace Estimation.Domain.Models
{
    public class GeneralPrintable
    {
        /// <summary>
        /// Gets the current date time.
        /// </summary>
        /// <value>
        /// The current date time.
        /// </value>
        public DateTime CurrentDateTime => DateTime.Now;
        /// <summary>
        /// Gets the data dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetDataDictionary()
        {
            var dataDict = new Dictionary<string, string>
            {
                {
                    "DateTime", CurrentDateTime.ToString("dd/MM/yyyy HH:mm")
                },
                {
                    "Date", CurrentDateTime.ToString("dd/MM/yyyy")
                },
                {
                    "LongDate", CurrentDateTime.ToString("dd/MMMM/yyyy")
                }
            };

            return dataDict;
        }
    }
}
