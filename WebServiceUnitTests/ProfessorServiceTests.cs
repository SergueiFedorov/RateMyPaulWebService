using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMPExtractor.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceUnitTests
{
    [TestClass]
    public class ProfessorServiceTests
    {
        [TestMethod]
        public void ProfessorGet()
        {
            ProfessorController professorController = new ProfessorController();
            dynamic result = professorController.Get(new Uri("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=461833"));
        }

        [TestMethod]
        public void SearchGet()
        {
            SearchController searchController = new SearchController();
            dynamic result = searchController.Get("Robin Burke");
        }

        [TestMethod]
        public void SearchGetInvalidName()
        {
            SearchController searchController = new SearchController();
            dynamic result = searchController.Get("1234");
        }
    }
}
