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
    public class RMPSearch : RMPParseRequest, ICachable
    {
        private const string queryString = "http://www.ratemyprofessors.com/search.jsp?queryoption=HEADER&queryBy=teacherName&schoolName=DePaul+University&schoolID=1389&query={0}";

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

        public override string GetObjectID()
        {
            byte[] pathBytes = Encoding.ASCII.GetBytes(_name);

            MD5 hash = MD5.Create();
            byte[] outputBytes = hash.ComputeHash(pathBytes, 0, pathBytes.Length);

            return string.Concat(outputBytes.Select(x => x.ToString("X2")));
        }
    }
}