using BlitzkriegLauncher.Models;
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
    public class ExeLauncher
    {
        private static string gameExeFilePath = AppDomain.CurrentDomain.BaseDirectory + "game.exe";
        private static string mapExeFilePath = AppDomain.CurrentDomain.BaseDirectory + "mapeditor.exe";

        public static void LaunchGame(GameLaunchOptions options) 
        {
            if (File.Exists(gameExeFilePath))
            {
                if(options.LaunchWindowed)
                    Process.Start(gameExeFilePath, "-Windowed");
                else
                    Process.Start(gameExeFilePath);

                Application.Current.Shutdown();
            }
            else
                MessageBox.Show("The game.exe file was not found, please place the launcher in the \n rootfolder of your Blitzkrieg folder.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void LaunchMapEditor() 
        {
            if (File.Exists(mapExeFilePath))
                Process.Start(mapExeFilePath);
            else
                MessageBox.Show("The mapeditor.exe file was not found.", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
