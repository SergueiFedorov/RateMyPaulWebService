using RMPExtractorLibrary;
using RMPExtractorLibrary.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RMPExtractor.Controllers
{
    public class ProfessorController : ApiController
    {
        // GET api/professor
        public dynamic Get(Uri url)
        {
            string cachedHTML = Cache.GetRecordHTML("http://www.ratemyprofessors.com/" + url.ToString());

            RMPProfessor professor = null;

            if (cachedHTML != null)
            {
                professor = RMPProfessor.GetFromString(cachedHTML);
            }
            else
            {
                professor = RMPProfessor.Get("http://www.ratemyprofessors.com/" + url.ToString());
                Cache.CacheHTMLRecord(professor);
            }

            return new
            {
                Grades = professor.Grades,
                Ratings = professor.Ratings,
            };
        }
    }
}
