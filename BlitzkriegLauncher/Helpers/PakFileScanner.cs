using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlitzkriegLauncher.Helpers
{
    public class PakFileScanner
    {
        private string baseFolder = AppDomain.CurrentDomain.BaseDirectory + "data//";
        private FileSystemWatcher pakWatcher;

        public PakFileScanner()
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

        #region "watcher events"

        private void pakWatcher_Created(object sender, FileSystemEventArgs e)
        {
            PakFileHandler.FileChanged(FileEventState.CREATED);
        }

        private void pakWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            PakFileHandler.FileChanged(FileEventState.CHANGED);
        }

        private void pakWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            PakFileHandler.FileChanged(FileEventState.DELETED);
        }

        #endregion
    }

    public enum FileEventState 
    { 
        CREATED,
        CHANGED,
        DELETED
    }

}
