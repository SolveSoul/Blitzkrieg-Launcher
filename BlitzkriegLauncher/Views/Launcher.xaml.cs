using BlitzkriegLauncher.Helpers;
using BlitzkriegLauncher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlitzkriegLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollectionExtended<PakFile> PakFiles { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PakFiles = PakFileHandler.LoadPakFiles();
            lstPakFiles.ItemsSource = PakFiles;
        }

        #region Window Events

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        private void ChangePakState(object sender, RoutedEventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            string name = (string)c.Tag;
            PakFile pakfileToHandle = PakFileHandler.FindPakFileByName(PakFiles, name);

            if (pakfileToHandle != null) 
                PakFileHandler.ChangePakFilesExtension(pakfileToHandle);
            
            
        }

    }
}
