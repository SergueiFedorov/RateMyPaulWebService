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
                HtmlNode sliderNodes = WebDocument.DocumentNode.Descendants()
                                                    .Where(node => node.Attributes["class"] != null
                                                           && node.Attributes["class"].Value == "faux-slides").First();

                return sliderNodes.Descendants().Where(node => node.Attributes["class"] != null &&  node.Attributes["class"].Value == "rating-slider")
                                  .Select(node => new RatingNode
                                   {
                                       Label = node.ChildNodes.Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "label").FirstOrDefault().InnerText,
                                       Rating = node.ChildNodes.Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value == "rating").FirstOrDefault().InnerText,
                                   });
            }

        }

        public IEnumerable<RatingNode> Grades
        {
            get
            {
                HtmlNode gradeNodes = WebDocument.DocumentNode.Descendants()
                                                    .Where(node => node.Attributes["class"] != null
                                                           && node.Attributes["class"].Value == "breakdown-wrapper").First();

                return gradeNodes.Descendants().Where(node => node.Attributes["class"] != null
                                                           && node.Attributes["class"].Value == "breakdown-header")
                                               .Select(node => new RatingNode
                                               {
                                                   Label = node.InnerText.Trim(),
                                                   Rating = node.Descendants().Where(innerNode => innerNode.Attributes["class"] != null && innerNode.Attributes["class"].Value == "grade").First().InnerText
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