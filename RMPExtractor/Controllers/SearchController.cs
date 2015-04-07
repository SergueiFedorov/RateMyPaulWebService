using RMPExtractorLibrary;
using RMPExtractorLibrary.Caching;
using RMPExtractorLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMPExtractor.Controllers
{
    public class SearchController : ApiController
    {
        // GET api/search/5
        public IEnumerable<ProfessorSearchResult> Get(string id)
        {
            string cachedHTML = Cache.GetRecordHTML(id);

            RMPSearch searchResult = null;

            if (cachedHTML != null)
            {
                searchResult = RMPSearch.GetFromString(cachedHTML);
            }
            else
            {
                searchResult = RMPSearch.Get(id);
                Cache.CacheHTMLRecord(searchResult);
            }

            return searchResult.Professors;
        }
    }
}
