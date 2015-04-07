using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMPExtractorLibrary;

namespace UnitTests
{
    [TestClass]
    public class Test_RMPReviews
    {
        [TestMethod]
        public void GetBurkeReviews()
        {
            RMPReviews reviews = RMPReviews.Get("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=461833");
            var result = reviews.AllReviews;
        }
    }
}
