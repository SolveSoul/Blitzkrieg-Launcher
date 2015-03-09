using System.Runtime.Serialization;

namespace BlitzkriegLauncher.Utilities
{
    public class XmlSerializer
    {
        public static void DeserializeXml<T>() where T : class
        {
            DataContractSerializer ds = new DataContractSerializer(typeof(T));
            
        }
    }
}