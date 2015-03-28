using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using RMPExtractorLibrary.Objects;

namespace RMPExtractorLibrary
{
    public class RMPSearch
    {
        private const string queryString = "http://www.ratemyprofessors.com/search.jsp?queryoption=HEADER&queryBy=teacherName&schoolName=DePaul+University&schoolID=1389&query={0}";

        public HtmlDocument WebDocument { get; private set; }
        public HtmlWeb htmlWeb;

        public IEnumerable<ProfessorSearchResult> Professors 
        {
            get
            {
                List<HtmlNode> professorNodes = WebDocument.DocumentNode.Descendants()
                                                .Where(node => node.Name == "li" && node.Attributes["class"] != null && node.Attributes["class"].Value == "listing PROFESSOR")
                                                .ToList();

                return professorNodes.Select(node => new ProfessorSearchResult
                                        {
                                            URL = node.Descendants().Where(innerNode => innerNode.Name == "a").FirstOrDefault().Attributes["href"].Value,
                                            College = node.Descendants().Where(innerNode => innerNode.Attributes["class"] != null && innerNode.Attributes["class"].Value == "sub").FirstOrDefault().InnerText,
                                            Name = node.Descendants().Where(innerNode => innerNode.Attributes["class"] != null && innerNode.Attributes["class"].Value == "main").FirstOrDefault().InnerText,
                                        });
            }
        }

        private RMPSearch(string name)
        {
            this.htmlWeb = new HtmlWeb();
            this.WebDocument = this.htmlWeb.Load(String.Format(queryString, name));
        }

        public static RMPSearch Get(string professorName)
        {
            return new RMPSearch(professorName);
        }

    }
}