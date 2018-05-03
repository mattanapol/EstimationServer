using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Estimation.Services.Helpers
{
    public class HtmlParser
    {
        /// <summary>
        /// Parse given html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="dataList"></param>
        /// <returns>Parsed Html</returns>
        public static string ParseHtml(string html, Dictionary<string, string> dataList)
        {
            foreach (var data in dataList)
            {
                html = html.Replace(data.Key, data.Value, StringComparison.Ordinal);
            }

            const string pattern = @"##\w+##";
            const string replacement = "";
            var rgx = new Regex(pattern);
            html = rgx.Replace(html, replacement);

            return html;
        }
    }
}
