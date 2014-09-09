using BlitzkriegLauncher.Helpers;
using BlitzkriegLauncher.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlitzkriegLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string baseFolder = AppDomain.CurrentDomain.BaseDirectory + "data//";
        private FileSystemWatcher pakWatcher;
        private static ObservableCollectionExtended<PakFile> PakFiles;

        public GameLaunchOptions LaunchOptions { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PakFiles = PakFileHandler.LoadPakFiles();
            this.LaunchOptions = new GameLaunchOptions();
            lstPakFiles.ItemsSource = PakFiles;
            InitPakFileScanner();
        }

        #region Window Events

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Control Events
        private void ChangePakState(object sender, RoutedEventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            string name = (string)c.Tag;
            PakFile pakfileToHandle = PakFileHandler.FindPakFileByName(PakFiles, name);

            if (pakfileToHandle != null) 
                PakFileHandler.ChangePakFilesExtension(pakfileToHandle);

        }

        private void LaunchGame(object sender, RoutedEventArgs e)
        {
            ExeLauncher.LaunchGame(LaunchOptions);
        }

        private void LaunchMapEditor(object sender, RoutedEventArgs e)
        {
            ExeLauncher.LaunchMapEditor();
        }

        #endregion

        #region "PakFileScanner Events"

        private void InitPakFileScanner()
        {
            if (Directory.Exists(baseFolder)) 
            {
                pakWatcher = new FileSystemWatcher(baseFolder);
                pakWatcher.Filter = "*.*pak";
                pakWatcher.Created += pakWatcher_Created;
                pakWatcher.Changed += pakWatcher_Changed;
                pakWatcher.Deleted += pakWatcher_Deleted;
                pakWatcher.EnableRaisingEvents = true;
            }
        }
        
        private void pakWatcher_Created(object sender, FileSystemEventArgs e)
        {
            PakFiles = PakFileHandler.LoadPakFiles();
            this.Dispatcher.Invoke((Action)(() =>{  lstPakFiles.ItemsSource = PakFiles; }));
        }

        private void pakWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            PakFiles = PakFileHandler.LoadPakFiles();
            this.Dispatcher.Invoke((Action)(() => { lstPakFiles.ItemsSource = PakFiles; }));
        }

        private void pakWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            PakFiles = PakFileHandler.LoadPakFiles();
            this.Dispatcher.Invoke((Action)(() => { lstPakFiles.ItemsSource = PakFiles; }));
        }

        #endregion

    }
}
