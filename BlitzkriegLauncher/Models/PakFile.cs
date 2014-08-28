using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlitzkriegLauncher.Models
{
    public class PakFile
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string FullPath { get; set; }

        public override string ToString()
        {
            return this.Name + " - " + this.IsActive;
        }

        public void ChangeExtension() 
        {
            if (this.IsActive)
                this.FullPath = this.FullPath.Replace(".inpak", ".pak");
            else
                this.FullPath = this.FullPath.Replace(".pak", ".inpak");
        }

    }

}
