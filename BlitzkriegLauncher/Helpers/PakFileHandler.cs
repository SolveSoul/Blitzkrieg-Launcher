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

            if (Directory.Exists(baseDir))
            {
                //get the paths of the files
                string[] activePaks = Directory.GetFiles(baseDir, "*.pak");
                string[] inactivePaks = Directory.GetFiles(baseDir, "*.inpak");

                //get the active files
                result.AddRange(GetPakFilesFromStringArray(activePaks, true));
                result.AddRange(GetPakFilesFromStringArray(inactivePaks, false));

                //remove all the .pak-files that may not be edited
                ObservableCollectionExtended<PakFile> paksToExclude = ReadPakExceptions();
                List<PakFile> paksToCheck = result.ToList();                                    //Whilst foreaching, the collection may not be changed, that's why we make a copy, also a list because an observablecollection copies the values

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

        private static ObservableCollectionExtended<PakFile> ReadPakExceptions() 
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "pakexceptions.xml";
            ObservableCollectionExtended<PakFile> result = new ObservableCollectionExtended<PakFile>();

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
                    PakFile file = new PakFile() { Name = node["name"].InnerText, IsActive = true, FullPath = baseDir + node["name"].InnerText, Description = node["description"].InnerText };

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
                MessageBox.Show("The file was not found, please restart the launcher.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }  
        }

        #endregion
    }
}
