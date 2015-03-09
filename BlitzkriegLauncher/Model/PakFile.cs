namespace BlitzkriegLauncher.Model
{
    public class PakFile
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsHidden { get; set; }
        public string FullPath { get; set; }

        public override string ToString()
        {
            return Name + " - " + IsActive;
        }

        public void ChangeExtension()
        {
            if (IsActive)
                FullPath = FullPath.Replace(".inpak", ".pak");
            else
                FullPath = FullPath.Replace(".pak", ".inpak");
        }
    }
}