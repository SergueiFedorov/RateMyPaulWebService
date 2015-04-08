using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary.Exceptions
{
    [Serializable]
    public class InvalidPageException : Exception
    {
        public string URL { get; private set; }

        public InvalidPageException(Exception throwingException, string url)
            : base("The document could not be obtained from the URL", throwingException)
        {
            this.URL = url;
        }
    }
}
