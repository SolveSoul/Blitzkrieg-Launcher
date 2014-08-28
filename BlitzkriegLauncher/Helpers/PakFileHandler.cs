using BlitzkriegLauncher.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace BlitzkriegLauncher.Helpers
{
    public class PakFileHandler
    {
        //fields
        private static string baseDir = AppDomain.CurrentDomain.BaseDirectory + "data//";

        #region loading files
        public static ObservableCollectionExtended<PakFile> LoadPakFiles()
        {
            ObservableCollectionExtended<PakFile> result = new ObservableCollectionExtended<PakFile>();

            //get the paths of the files
            string[] activePaks = Directory.GetFiles(baseDir, "*.pak");
            string[] inactivePaks = Directory.GetFiles(baseDir, "*.inpak");

            //get the active files
            result.AddRange(GetPakFilesFromStringArray(activePaks, true));
            result.AddRange(GetPakFilesFromStringArray(inactivePaks, false));

            ReadPakExceptions();

            return result;
        }

        //helpers
        private static ObservableCollectionExtended<PakFile> GetPakFilesFromStringArray(string[] pakFiles, bool isActive) 
        {
            ObservableCollectionExtended<PakFile> result = new ObservableCollectionExtended<PakFile>();

            foreach (string pakPath in pakFiles)
            {
                PakFile pak = new PakFile() { Name = Path.GetFileNameWithoutExtension(pakPath), FullPath = pakPath, IsActive = isActive };
                result.Add(pak);
            }

            return result;
        }

        private static IEnumerable<PakFile> ReadPakExceptions() 
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "pakexceptions.xml";
            ObservableCollectionExtended<PakFile> result = new ObservableCollectionExtended<PakFile>();

            if (!File.Exists(xmlPath))
                MessageBox.Show("Couldn't load the pakfiles to exclude, loading all pak-files...", "pakexceptions.xml not found", MessageBoxButton.OK, MessageBoxImage.Error);

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);

            XmlNodeList nodes = xdoc.SelectNodes("/pakexceptions/pak");

            foreach (XmlNode node in nodes)
            {
                PakFile file = new PakFile() { Name = node.InnerText, IsActive = true, FullPath = baseDir + node.InnerText };
                result.Add(file);
            }

            return result;
        }

        #endregion

        #region finding files
        public static PakFile FindPakFileByName(IEnumerable<PakFile> files, string name) 
        {
            foreach (PakFile pf in files)
                if (pf.Name == name)
                    return pf;
            
            return null;
        }
        #endregion

        #region changing files

        public static void ChangePakFilesExtension(PakFile file) 
        {
            try
            {
                if (!file.IsActive)
                {
                    File.Move(file.FullPath, Path.ChangeExtension(file.FullPath, ".inpak"));
                    file.ChangeExtension();

                }
                else
                {
                    File.Move(file.FullPath, Path.ChangeExtension(file.FullPath, ".pak"));
                    file.ChangeExtension();
                }
            }
            catch
            {
                MessageBox.Show("The file was not found, please restart the launcher.", "File not found");
            }  
        }

        #endregion
    }
}
