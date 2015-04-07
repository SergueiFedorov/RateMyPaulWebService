using HtmlAgilityPack;
using RMPExtractorLibrary.Caching;
using RMPExtractorLibrary.Exceptions;
using RMPExtractorLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary
{
    public class RMPReviews : RMPParseRequest
    {
        private RMPReviews(string URL)
            : base(URL)
        {

        }

        public static RMPReviews Get(string URL)
        {
            return new RMPReviews(URL);
        }

        public IEnumerable<ProfessorReviewResult> AllReviews
        {
            get
            {
                HtmlNode sliderNodes = RMPParsingTools.GetNodesByClass(WebDocument.DocumentNode.Descendants(), "comments").FirstOrDefault();

                if (sliderNodes == null)
                {
                    return new List<ProfessorReviewResult>();
                }


                return null;
            }
        }

    }
}
