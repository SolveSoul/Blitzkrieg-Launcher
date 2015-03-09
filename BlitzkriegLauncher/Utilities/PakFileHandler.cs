using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using BlitzkriegLauncher.Model;

namespace BlitzkriegLauncher.Utilities
{
    public class PakFileHandler
    {
        //fields
        private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory + "data//";


        #region loading files

        public static IEnumerable<PakFile> LoadPakFiles()
        {
            List<PakFile> result = new List<PakFile>();

            if (Directory.Exists(BaseDir))
            {
                //get the paths of the files
                string[] activePaks = Directory.GetFiles(BaseDir, "*.pak");
                string[] inactivePaks = Directory.GetFiles(BaseDir, "*.inpak");

                //get the active files
                result.AddRange(GetPakFilesFromStringArray(activePaks, true));
                result.AddRange(GetPakFilesFromStringArray(inactivePaks, false));

                //remove all the .pak-files that may not be edited
                List<PakFile> paksToExclude = ReadPakExceptions().ToList();
                List<PakFile> paksToCheck = result.ToList();

                foreach (PakFile exc in paksToExclude)
                    foreach (PakFile p in paksToCheck)
                        if (exc.Name == p.Name)
                        {
                            if (exc.IsHidden)                                                   //remove when the pak needs to be excluded
                                result.Remove(p);
                            else
                                p.Description = exc.Description;                                //when the pak doesn't need hiding, the description can be loaded
                        }


            }
            else
            {
                MessageBox.Show("No 'data' folder found. Place the launcher .exe inside your BlitzKrieg folder! \n", "No 'data' folder found.", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }


            return result;
        }

        private static IEnumerable<PakFile> GetPakFilesFromStringArray(string[] pakFiles, bool isActive)
        {
            return pakFiles.Select(pakPath => new PakFile {Name = Path.GetFileNameWithoutExtension(pakPath), FullPath = pakPath, IsActive = isActive });
        }

        private static IEnumerable<PakFile> ReadPakExceptions() 
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "pakexceptions.xml";
            List<PakFile> result = new List<PakFile>();

            if (!File.Exists(xmlPath)) 
            {
                MessageBox.Show("Couldn't load the pakfiles to exclude, please restore the 'pakexceptions.xml' file", "pakexceptions.xml not found", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);

            try
            {
                XmlNodeList nodes = xdoc.SelectNodes("/pakexceptions/pak");

                foreach (XmlNode node in nodes)
                {
                    PakFile file = new PakFile() { Name = node["name"].InnerText, IsActive = true, FullPath = BaseDir + node["name"].InnerText, Description = node["description"].InnerText };

                    if (Convert.ToBoolean(node["exclude"].InnerText))
                        file.IsHidden = true;
                    else
                        file.IsHidden = false;

                    result.Add(file);
                }
            }
            catch
            {
                MessageBox.Show("The XML file is corrupt, please restore the 'pakexceptions.xml' file.", "XML file is corrupt", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }


            return result;
        }

        #endregion

        #region finding files

        public static PakFile FindPakFileByName(IEnumerable<PakFile> files, string name)
        {
            foreach (var pf in files)
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
                MessageBox.Show("The file was not found, please restart the launcher.", "File not found",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}