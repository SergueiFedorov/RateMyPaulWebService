using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary.Caching
{
    public interface ICachable
    {
        string GetObjectID();
        string GetDataToCache();
    }
}
