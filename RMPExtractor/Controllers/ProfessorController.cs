using RMPExtractorLibrary;
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
            RMPProfessor professor = RMPProfessor.Get("http://www.ratemyprofessors.com/" + url.ToString());

            return new
            {
                Grades = professor.Grades,
                Ratings = professor.Ratings,
            };
        }
    }
}
