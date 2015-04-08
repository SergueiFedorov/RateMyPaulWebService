using HtmlAgilityPack;
using RMPExtractorLibrary.Caching;
using RMPExtractorLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary
{
    public abstract class RMPParseRequest : ICachable
    {
        public HtmlDocument WebDocument { get; private set; }
        public HtmlWeb htmlWeb;
        protected bool _URLFetchFailed = false;

        protected RMPParseRequest(HtmlDocument document)
        {
            this.WebDocument = document;

        }

        protected RMPParseRequest(string url)
        {
            if (url.ToLower().Contains("ratemyprofessors.com") == false)
            {
                throw new IsNotRMPURLException();
            }

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

                throw;
            }
        }

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

        public virtual string GetObjectID()
        {
            byte[] pathBytes = Encoding.ASCII.GetBytes(this.htmlWeb.ResponseUri.OriginalString);

            MD5 hash = MD5.Create();
            byte[] outputBytes = hash.ComputeHash(pathBytes, 0, pathBytes.Length);

            return string.Concat(outputBytes.Select(x => x.ToString("X2")));
        }

        public virtual string GetDataToCache()
        {
            return this.WebDocument.DocumentNode.OuterHtml;
        }
    }
}
