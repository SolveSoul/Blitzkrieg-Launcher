using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlitzkriegLauncher.Helpers
{
    public class ConfigHandler
    {

        private const string configFile = AppDomain.CurrentDomain.BaseDirectory + "config.cfg";

        public static void ReadConfigFile() 
        {
            if (File.Exists(configFile))
            {

            }
            else
                MessageBox.Show("Could not find the 'config.cfg' file.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
