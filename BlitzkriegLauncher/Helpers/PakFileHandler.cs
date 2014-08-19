using BlitzkriegLauncher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlitzkriegLauncher.Helpers
{
    public class PakFileHandler
    {

        private static string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        public static ObservableCollectionExtended<PakFile> LoadPakFiles()
        {
            ObservableCollectionExtended<PakFile> result = new ObservableCollectionExtended<PakFile>();

            //get the paths of the files
            string[] activePaks = Directory.GetFiles(baseDir, "*.pak");
            string[] inactivePaks = Directory.GetFiles(baseDir, "*.inpak");

            //get the active files
            result.AddRange(GetPakFilesFromStringArray(activePaks, true));
            result.AddRange(GetPakFilesFromStringArray(inactivePaks, false));

            return result;
        }

        //helpers
        private static ObservableCollectionExtended<PakFile> GetPakFilesFromStringArray(string[] pakFiles, bool isActive) 
        {
            ObservableCollectionExtended<PakFile> result = new ObservableCollectionExtended<PakFile>();

            foreach (string pakPath in pakFiles)
            {
                PakFile pak = new PakFile() { Name = Path.GetFileNameWithoutExtension(pakPath), FullPath = pakPath, IsActive = true };
                result.Add(pak);
            }

            return result;
        }

    }
}
