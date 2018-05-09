using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Estimation.Domain;
using HtmlAgilityPack;

namespace Estimation.Services.Helpers
{
    public class HtmlParser
    {
        public const string CommandPattern = @"##\w+##";

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
            
            const string replacement = "";
            var rgx = new Regex(CommandPattern);
            html = rgx.Replace(html, replacement);

            return html;
        }

        /// <summary>
        /// Parses the HTML node by class.
        /// </summary>
        /// <param name="rootNode">The root node.</param>
        /// <param name="targetClass">The target class.</param>
        /// <param name="printableObjectList">The printable object list.</param>
        /// <returns></returns>
        public static HtmlNode ParseHtmlNodeByClass(HtmlNode rootNode, string targetClass, IEnumerable<IPrintable> printableObjectList)
        {
            var rowTemplate = rootNode.Descendants()
                .First(n => n.GetAttributeValue("class", "").Equals(targetClass));
            var rowParent = rowTemplate.ParentNode;

            rowParent.RemoveChild(rowTemplate);
            foreach (var printable in printableObjectList)
            {
                var mainMaterialRow = rowTemplate.Clone();
                mainMaterialRow.InnerHtml = ParseHtml(mainMaterialRow.InnerHtml, printable.GetDataDictionary());
                rowParent.AppendChild(mainMaterialRow);
            }

            return rowTemplate;
        }
    }
}
