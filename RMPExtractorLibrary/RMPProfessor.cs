using HtmlAgilityPack;
using RMPExtractorLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMPExtractorLibrary
{
    public class RMPProfessor
    {
        public HtmlDocument WebDocument { get; private set; }
        public HtmlWeb htmlWeb;

        public IEnumerable<ProfessorRatingResult> Ratings
        {
            get
            {
                HtmlNode sliderNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(), "faux-slides").First();

                return  RMPParsingTools.GetNodesByClass(sliderNodes.Descendants(), "rating-slider")
                                       .Select(node => new ProfessorRatingResult
                                        {
                                            Label = RMPParsingTools.GetNodeValueByClass(node.ChildNodes, "label"),
                                            Rating = RMPParsingTools.GetNodeValueByClass(node.ChildNodes, "rating"),
                                        });
            }

        }

        public IEnumerable<ProfessorRatingResult> Grades
        {
            get
            {
                HtmlNode gradeNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(),  "breakdown-wrapper").First();

                return RMPParsingTools.GetNodesByClass(gradeNodes.Descendants(), "breakdown-header")
                                        .Select(node => new ProfessorRatingResult
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