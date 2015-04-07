using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMPExtractor.Controllers;
using System.Net;
using System.IO;

namespace WebServiceUnitTests
{
    [TestClass]
    public class ProfessorRemoteTests
    {
        public string GetResultFromHTTP(string address)
        {
            WebRequest request = WebRequest.Create(address);
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [TestMethod]
        public void GetExistingProfessor()
        {
            const string correctResponse = "[{\"Name\":\"Burke, Robin\",\"College\":\"DePaul University, Computer Science\",\"URL\":\"/ShowRatings.jsp?tid=461833\"},{\"Name\":\"Burke, James\",\"College\":\"DePaul University, Chemistry\",\"URL\":\"/ShowRatings.jsp?tid=1824173\"},{\"Name\":\"Burke, Kathryn\",\"College\":\"DePaul University, Philosophy\",\"URL\":\"/ShowRatings.jsp?tid=1188114\"}]";

            //Points at the test API
            const string address = "http://sergueifedorov.com/rmpapi/test/search/burke";

            string response = GetResultFromHTTP(address);

            Assert.AreEqual(correctResponse, response);
        }
    }
}
