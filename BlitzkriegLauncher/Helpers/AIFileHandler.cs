using BlitzkriegLauncher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlitzkriegLauncher.Helpers
{
    public class AIFileHandler
    {
        private static string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        public static List<AIFile> GetActiveAIFiles() 
        {
            List<AIFile> result = new List<AIFile>();

            foreach(string filename in Directory.GetFiles(baseDir))
                if (filename.Contains("ailogic")) 
                    result.Add(CreateAIFile(filename));

            return result;
        }

        private static AIFile CreateAIFile(string filename)
        {
            AIFile result = new AIFile(filename);
            



            return result;
        }

    }
}
