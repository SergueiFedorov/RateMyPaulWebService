using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using RMPExtractorLibrary.Objects;
using RMPExtractorLibrary.Caching;
using System.Security.Cryptography;
using System.Text;

namespace RMPExtractorLibrary
{
    public class RMPSearch : RMPParseRequest
    {
        private const string queryString = "http://www.ratemyprofessors.com/search.jsp?queryoption=HEADER&queryBy=teacherName&schoolName=DePaul+University&schoolID=1389&query={0}";

        public IEnumerable<ProfessorSearchResult> Professors 
        {
            get
            {
                /*List<HtmlNode> professorNodes = WebDocument.DocumentNode.Descendants()
                                                .Where(node => node.Attributes["class"] != null && node.Attributes["class"].Value == "listing PROFESSOR")
                                                .ToList();*/


                var professorNodes = WebDocument.DocumentNode.SelectNodes("//li[contains(@class, 'listing PROFESSOR')]");

                if (professorNodes == null)
                {
                    return null;
                }

                return professorNodes.Select(node => new ProfessorSearchResult
                                        {
                                            URL = node.LastChild.Attributes["href"].Value,
                                           // College = node.Descendants("span").Where(innerNode => innerNode.Attributes["class"] != null && innerNode.Attributes["class"].Value == "sub").First().InnerText,
                                            College = node.SelectNodes("//span[contains(@class, 'sub')]").First().InnerText,
                                            //Name = node.Descendants("span").Where(innerNode => innerNode.Attributes["class"] != null && innerNode.Attributes["class"].Value == "main").First().InnerText,
                                            Name = node.SelectNodes("//span[contains(@class, 'main')]").First().InnerText
                                        });
            }
        }

        private string _name;

        private RMPSearch(string name)
            : base(String.Format(queryString, name))
        {
            _name = name;
        }

        private RMPSearch(HtmlDocument document)
            : base(document)
        {

        }

        public static RMPSearch Get(string professorName)
        {
            return new RMPSearch(professorName);
        }

        public static RMPSearch GetFromString(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            return new RMPSearch(document);
        }
    }
}