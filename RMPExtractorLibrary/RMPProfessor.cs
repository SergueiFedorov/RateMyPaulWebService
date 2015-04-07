using HtmlAgilityPack;
using RMPExtractorLibrary.Caching;
using RMPExtractorLibrary.Exceptions;
using RMPExtractorLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Text;
using System.IO;

namespace RMPExtractorLibrary
{
    public class RMPProfessor : RMPParseRequest
    {
        private RMPProfessor(string url)
            : base(url)
        {

        }

        private RMPProfessor(HtmlDocument document)
            : base(document)
        {

        }

        public IEnumerable<ProfessorRatingResult> Ratings
        {
            get
            {
                if (this.IsValid == false)
                {
                    return null;
                }

                HtmlNode sliderNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(), "faux-slides").FirstOrDefault();

                if (sliderNodes == null)
                {
                    //Return empty grades
                    return DefaultInitializations.DefaultProfessorRatings;
                }


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

                HtmlNode gradeNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(),  "breakdown-wrapper").FirstOrDefault();

                //Grades are not found
                if (gradeNodes == null)
                {
                    return DefaultInitializations.DefaultProfessorGrades;
                }

                return RMPParsingTools.GetNodesByClass(gradeNodes.Descendants(), "breakdown-header")
                                        .Select(node => new ProfessorRatingResult
                                        {
                                            Label = node.InnerText.Trim(),
                                            Rating = RMPParsingTools.GetNodeValueByClass(node.Descendants(), "grade")
                                        });
            }
        }

        public static RMPProfessor Get(string url)
        {
            return new RMPProfessor(url);
        }

        public static RMPProfessor GetFromString(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            return new RMPProfessor(document);
        }
    }
}