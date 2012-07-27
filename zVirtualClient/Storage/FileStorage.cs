using System;
using System.IO;

namespace zVirtualClient.Storage
{
    /// <summary>
    /// A FileStorage Device.
    /// </summary>
    public class FileStorage : IStoreage
    {

        public void Save(string file, System.IO.MemoryStream contents)
        {
            if (contents.CanSeek && contents.Position > 0) contents.Seek(0, SeekOrigin.Begin);
            System.IO.File.WriteAllBytes(file, contents.ToArray());
        }
        public System.IO.MemoryStream Load(string file)
        {
            return new System.IO.MemoryStream(System.IO.File.ReadAllBytes(file));
        }



        public bool FileExists(string file)
        {
            return System.IO.File.Exists(file);
        }
    }
}
