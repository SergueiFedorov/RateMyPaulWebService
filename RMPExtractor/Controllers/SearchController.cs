﻿using RMPExtractor.Code;
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
            RMPSearch searchResult = RMPSearch.InvokeSearch(id);
            return searchResult.Professors;
        }
    }
}