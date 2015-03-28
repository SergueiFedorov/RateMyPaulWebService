using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary.Objects
{
    public class ProfessorPageResult
    {
        public string Name { get; set; }

        //No need to parse down to a float. Wasted effort only to convert it back to string for JSON
        public string Helpfullness { get; set; }
        public string Clarity { get; set; }
        public string Easiness { get; set; }
    }
}
