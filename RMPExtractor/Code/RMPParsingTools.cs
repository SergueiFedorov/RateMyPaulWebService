using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMPExtractor.Code
{
    internal static class RMPParsingTools
    {
        public static bool CheckIfNodeHasClass(HtmlNode node, string className)
        {
            return node.Attributes["class"] != null && node.Attributes["class"].Value == className;
        }

        public static string GetNodeValueByClass(IEnumerable<HtmlNode> nodes, string className)
        {
            HtmlNode discoveredNode = nodes.Where(node => CheckIfNodeHasClass(node, className)).FirstOrDefault();

            if (discoveredNode == null)
            {
                return null;
            }

            return discoveredNode.InnerText;
        }

        public static IEnumerable<HtmlNode> GetNodesByClass(IEnumerable<HtmlNode> nodes, string className)
        {
            return nodes.Where(node => CheckIfNodeHasClass(node, className));
        }

    }
}