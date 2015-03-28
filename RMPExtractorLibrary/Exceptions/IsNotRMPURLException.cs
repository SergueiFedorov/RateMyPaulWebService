using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary.Exceptions
{
    public class IsNotRMPURLException : Exception
    {
        public IsNotRMPURLException()
            : base("The URL provided does not contain ratemyprofessors.com")
        {
            
        }
    }
}
