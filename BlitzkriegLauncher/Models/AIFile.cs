using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlitzkriegLauncher.Models
{
    public class AIFile
    {
        //props
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public long FileSize { get; set; }
        public string FileExtension { get; set; }

        //ctor
        public AIFile(string fullPath)
        {
            this.FileSize = GetFileSize(fullPath);
            this.FileExtension = GetFileExtension(fullPath);
        }

        //getters & setters
        public long GetFileSize(string fullPath) 
        {
            if (File.Exists(fullPath))
                return new FileInfo(fullPath).Length;
            else
                return 0;
        }

        public string GetFileExtension(string fullPath) 
        {
            if (File.Exists(fullPath))
                return new FileInfo(fullPath).Extension;
            else
                return String.Empty;
        }
    }
}
