using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMPExtractor.Code
{
    public class ProfessorPageResult
    {
        public string Name { get; set; }

        //No need to parse down to a float. Wasted effort only to convert it back to string for JSON
        public string Helpfullness { get; set; }
        public string Clarity { get; set; }
        public string Easiness { get; set; }
    }

    public class RatingNode
    {
        public string Label { get; set; }
        public string Rating { get; set; }
    }

    public class RMPProfessor
    {
        public HtmlDocument WebDocument { get; private set; }
        public HtmlWeb htmlWeb;

        public IEnumerable<RatingNode> Ratings
        {
            get
            {
                HtmlNode sliderNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(), "faux-slides").First();

                return sliderNodes.Descendants().Where(node => RMPParsingTools.CheckIfNodeHasClass(node, "rating-slider"))
                                  .Select(node => new RatingNode
                                   {

                                       Label = RMPParsingTools.GetNodeValueByClass(node.ChildNodes, "label"),
                                       Rating = RMPParsingTools.GetNodeValueByClass(node.ChildNodes, "rating"),
                                   });
            }

        }

        public IEnumerable<RatingNode> Grades
        {
            get
            {
                HtmlNode gradeNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(),  "breakdown-wrapper").First();

                return RMPParsingTools.GetNodesByClass(gradeNodes.Descendants(), "breakdown-header")
                                        .Select(node => new RatingNode
                                        {
                                            Label = node.InnerText.Trim(),
                                            Rating = RMPParsingTools.GetNodeValueByClass(node.Descendants(), "grade")
                                        });
            }
        }

        private RMPProfessor(string url)
        {
            this.htmlWeb = new HtmlWeb();
            this.WebDocument = this.htmlWeb.Load(url);
        }

        public static RMPProfessor Get(string url)
        {
            return new RMPProfessor(url);
        }
    }
}