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
        private const string TemplateClassName = "template";
        public const string ContentsClassName = "contents";

        /// <summary>
        /// Parse given html
        /// </summary>
        /// <param name="html"></param>
        /// <param name="dataList"></param>
        /// <param name="clearHtml"></param>
        /// <returns>Parsed Html</returns>
        public static string ParseHtml(string html, Dictionary<string, string> dataList, bool clearHtml = true)
        {
            foreach (var data in dataList)
            {
                html = html.Replace(data.Key, data.Value, StringComparison.Ordinal);
            }

            if (clearHtml)
                html = Clear(html);

            return html;
        }

        /// <summary>
        /// Clears the specified HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string Clear(string html)
        {
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
                .FirstOrDefault(n =>
                {
                    var classes = n.GetClasses();
                    return classes.Any(e => e == TemplateClassName) &&
                           classes.Any(e => e == printableObjectList.FirstOrDefault()?.TargetClass);
                });
            if (rowTemplate == null)
                return rootNode;
            var rowParent = rowTemplate.ParentNode;
            var lastNode = rowTemplate;

            foreach (var printable in printableObjectList)
            {
                var contentRow = rowTemplate.Clone();
                contentRow.Attributes["class"].Value = contentRow.GetClasses()
                    .Where(e => e != TemplateClassName).Aggregate((current, next) => current + ' ' + next);

                var contentElements =
                    contentRow.GetClasses().Any(e => e == ContentsClassName)
                        ? new List<HtmlNode>{contentRow}
                        : contentRow.Descendants()
                            .Where(n =>
                            {
                                var classes = n.GetClasses();
                                return classes.Any(e => e == ContentsClassName) &&
                                       classes.Any(e => e == printable.TargetClass);
                            });

                foreach (var contentElement in contentElements)
                    contentElement.InnerHtml = ParseHtml(contentElement.InnerHtml, printable.GetDataDictionary());
                //if (contentElement != null)
                //    contentElement.InnerHtml = ParseHtml(contentElement.InnerHtml, printable.GetDataDictionary());
                //else
                //    contentRow.InnerHtml = ParseHtml(contentRow.InnerHtml, printable.GetDataDictionary());

                if (printable.Child != null && printable.Child.Count() != 0)
                    ParseHtmlNodeByClass(contentRow, "", printable.Child);

                rowParent.InsertAfter(contentRow, lastNode);
                lastNode = contentRow;
            }

            rowParent.RemoveChild(rowTemplate);
            RemoveAllTemplate(rootNode);

            return rootNode;
        }

        //private static string[] GetClasses(HtmlNode node)
        //{
        //    return node.GetAttributeValue("class", "").Split(' ');
        //}

        private static void RemoveAllTemplate(HtmlNode node)
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (node.ChildNodes[i].GetClasses().All(e => e != TemplateClassName))
                {
                    if (node.ChildNodes[i].HasChildNodes)
                        RemoveAllTemplate(node.ChildNodes[i]);
                }
                else
                {
                    node.RemoveChild(node.ChildNodes[i]);
                    i--;
                }
            }
        }
    }
}
