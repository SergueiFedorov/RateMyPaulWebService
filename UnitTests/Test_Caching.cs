using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMPExtractorLibrary;

namespace UnitTests
{
    /// <summary>
    /// Summary description for Test_Caching
    /// </summary>
    [TestClass]
    public class Test_Caching
    {

        [TestMethod]
        public void BasicHashing()
        {
            //
            // TODO: Add test logic here
            //

            RMPProfessor professor = RMPProfessor.Get("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=461833");
            string ID = professor.GetObjectID();



        }
    }
}
