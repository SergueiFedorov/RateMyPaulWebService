using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMPExtractorLibrary;
using System.Collections;
using System.Collections.Generic;
using RMPExtractorLibrary.Objects;
using RMPExtractorLibrary.Exceptions;

namespace UnitTests
{
    [TestClass]
    public class Test_RMPSearch
    {
        [TestMethod]
        public void GetProfessorSearchBurke()
        {
            RMPSearch search = RMPSearch.Get("Burke");

            List<ProfessorSearchResult> professors = search.Professors.ToList();

            Assert.IsNotNull(professors);
            Assert.IsTrue(professors.Count > 0);
            Assert.IsTrue(professors.Count == 3);
        }

        [TestMethod]
        public void GetProfessorPageBurke()
        {
            RMPProfessor professor = RMPProfessor.Get("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=461833");
            List<ProfessorRatingResult> ratings = professor.Ratings.ToList();

            Assert.IsNotNull(ratings);
            Assert.IsTrue(professor.IsValid);
            Assert.IsTrue(ratings.Count > 0);
            Assert.IsTrue(ratings.Count == 3);

            Assert.IsTrue(ratings.Any(rating => rating.Label == "Helpfulness"));
            Assert.IsTrue(ratings.Any(rating => rating.Label == "Clarity"));
            Assert.IsTrue(ratings.Any(rating => rating.Label == "Easiness"));

            List<ProfessorRatingResult> grades = professor.Grades.ToList();

            Assert.IsNotNull(grades);
            Assert.IsTrue(grades.Count > 0);
            Assert.IsTrue(grades.Count == 3);

        }

        [TestMethod]
        public void GetInvalidProfessor()
        {
            RMPProfessor professor = RMPProfessor.Get("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=123");
            Assert.IsFalse(professor.IsValid);

            //Attempt to obtain grades
            Assert.IsNull(professor.Grades);
            Assert.IsNull(professor.Ratings);
        }

        [TestMethod]
        [ExpectedException(typeof(IsNotRMPURLException))]
        public void GetProfessorInvalidURL()
        {
            //Expects an exception
            RMPProfessor professor = RMPProfessor.Get("MALFORMED_URL");
        }

        [TestMethod]
        [ExpectedException(typeof(IsNotRMPURLException))]
        public void GetSlightlyMalsformedProfessorURL()
        {
            RMPProfessor professor = RMPProfessor.Get("http://www.ratemyprofesss.com/ShowRatings.jsp?tid=461833");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPageException))]
        public void GetInvalidRMPProfessorPage()
        {
            RMPProfessor professor = RMPProfessor.Get("httpr://www.ratemyprofessors.com/ShowRatigs.jsp?tid=123");
        }
    }
}
