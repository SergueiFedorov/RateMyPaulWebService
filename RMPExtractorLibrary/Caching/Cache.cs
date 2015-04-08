using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RMPExtractorLibrary.Caching
{
    public static class Cache
    {
        public static void CacheHTMLRecord(ICachable cachableObject)
        {
            if (Directory.Exists("CachedRecords") == false)
            {
                Directory.CreateDirectory("CachedRecords");
            }

            string objectID = cachableObject.GetObjectID();

            using (FileStream fStream = File.Create(objectID))
            {
                StreamWriter writter = new StreamWriter(fStream);
                writter.Write(cachableObject.GetDataToCache());
            }
        }

        public static string GetRecordHTML(string recordID)
        {
            byte[] pathBytes = Encoding.ASCII.GetBytes(recordID);

            MD5 hash = MD5.Create();
            byte[] outputBytes = hash.ComputeHash(pathBytes, 0, pathBytes.Length);

            string fileName = string.Concat(outputBytes.Select(x => x.ToString("X2")));

            if (File.Exists(fileName))
            {
                if ((DateTime.Now - File.GetCreationTime(fileName)).Hours > 24)
                {
                    File.Delete(fileName);
                    return null;
                }

                string result = string.Empty;

                using (FileStream fstream = File.Open(fileName, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(fstream);
                    result = reader.ReadToEnd();
                }

                return result;
            }

            return null;
        }
    }
}
