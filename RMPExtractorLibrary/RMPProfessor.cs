using HtmlAgilityPack;
using RMPExtractorLibrary.Exceptions;
using RMPExtractorLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                if (this.IsValid == false)
                {
                    return null;
                }

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
                if (this.IsValid == false)
                {
                    return null;
                }

                HtmlNode gradeNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(),  "breakdown-wrapper").First();

                return RMPParsingTools.GetNodesByClass(gradeNodes.Descendants(), "breakdown-header")
                                        .Select(node => new ProfessorRatingResult
                                        {
                                            Label = node.InnerText.Trim(),
                                            Rating = RMPParsingTools.GetNodeValueByClass(node.Descendants(), "grade")
                                        });
            }
        }

        private bool _URLFetchFailed = false;

        public bool IsValid
        {
            get
            {
                if (_URLFetchFailed)
                {
                    return false;
                }

                List<HtmlNode> errorNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(), "header error").ToList();
                return errorNodes.Count == 0;
            }
        }

        private RMPProfessor(string url)
        {
            this.htmlWeb = new HtmlWeb();

            try
            {
                this.WebDocument = this.htmlWeb.Load(url);
            }
            catch (Exception ex)
            {
                if (ex is System.UriFormatException || ex is HtmlAgilityPack.HtmlWebException)
                {
                    _URLFetchFailed = true;
                    throw new InvalidPageException(ex, url);
                }

                throw ex;
            }
        }

        public static RMPProfessor Get(string url)
        {
            if (url.ToLower().Contains("ratemyprofessors.com") == false)
            {
                throw new IsNotRMPURLException();
            }

            return new RMPProfessor(url);
        }
    }
}