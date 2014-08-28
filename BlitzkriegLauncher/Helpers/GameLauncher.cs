using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlitzkriegLauncher.Helpers
{
    public class GameLauncher
    {
        private static string exeFilePath = AppDomain.CurrentDomain.BaseDirectory + "game.exe";

        public static void LaunchGame() 
        {
            if (File.Exists(exeFilePath))
            {
                Process.Start(exeFilePath);
                Application.Current.Shutdown();
            }
            else 
            {
                MessageBox.Show("The game.exe file was not found, please place the launcher in the \n rootfolder of your Blitzkrieg folder.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
