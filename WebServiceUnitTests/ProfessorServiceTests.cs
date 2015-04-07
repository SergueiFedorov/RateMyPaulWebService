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
        public void ProfessorGet_Test()
        {
            ProfessorController professorController = new ProfessorController();
            dynamic result = professorController.Get(new Uri("http://www.ratemyprofessors.com/ShowRatings.jsp?tid=461833"));
        }

        [TestMethod]
        public void SearchGet_Test()
        {
            SearchController searchController = new SearchController();
            dynamic result = searchController.Get("Rubin Burke");
        }
    }
}
