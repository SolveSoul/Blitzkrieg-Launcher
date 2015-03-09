using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using BlitzkriegLauncher.Model;
using BlitzkriegLauncher.Utilities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace BlitzkriegLauncher.ViewModel
{
    public class LauncherVM : ViewModelBase
    {

        #region props & fields

        private readonly string _baseFolder = AppDomain.CurrentDomain.BaseDirectory + "data//";
        private FileSystemWatcher _pakWatcher;

        private ObservableCollection<PakFile> _pakFiles;

        public ObservableCollection<PakFile> PakFiles
        {
            get { return _pakFiles; }
            set { _pakFiles = value; RaisePropertyChanged(); }
        }

        private PakFile _selectedPakFile;

        public PakFile SelectedPakFile
        {
            get { return _selectedPakFile; }
            set { _selectedPakFile = value; RaisePropertyChanged(); }
        }
        

        #endregion

        #region ctor(s)

        public LauncherVM()
        {
            PakFiles = new ObservableCollection<PakFile>(PakFileHandler.LoadPakFiles());
            InitPakFileScanner();
        }

        #endregion

        #region commands

        public ICommand PakFileCheckedCommand
        {
            get { return new RelayCommand(PakFileChecked); }
        }

        private void PakFileChecked()
        {
            var pakfileToHandle = PakFileHandler.FindPakFileByName(PakFiles, "");

            if (pakfileToHandle != null)
                PakFileHandler.ChangePakFilesExtension(pakfileToHandle); 
        }


        public ICommand LaunchMapEditorCommand
        {
            get { return new RelayCommand(LaunchMapEditor); }
        }

        private void LaunchMapEditor()
        {
            ExeLauncher.LaunchMapEditor();
        }

        public ICommand LaunchGameCommand
        {
            get { return new RelayCommand(LaunchGame); }
        }

        private void LaunchGame()
        {
            ExeLauncher.LaunchGame(new GameLaunchOptions());
        }

        #endregion

        #region "PakFileScanner Events"

        private void InitPakFileScanner()
        {
            if (!Directory.Exists(_baseFolder)) return;
            _pakWatcher = new FileSystemWatcher(_baseFolder) {Filter = "*.*pak"};
            _pakWatcher.Created += pakWatcher_Created;
            _pakWatcher.Changed += pakWatcher_Changed;
            _pakWatcher.Deleted += pakWatcher_Deleted;
            _pakWatcher.EnableRaisingEvents = true;
        }

        private void pakWatcher_Created(object sender, FileSystemEventArgs e)
        {
            PakFiles = new ObservableCollection<PakFile>(PakFileHandler.LoadPakFiles());
        }

        private void pakWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            PakFiles = new ObservableCollection<PakFile>(PakFileHandler.LoadPakFiles());
        }

        private void pakWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            PakFiles = new ObservableCollection<PakFile>(PakFileHandler.LoadPakFiles());
        }

        #endregion
    }
}