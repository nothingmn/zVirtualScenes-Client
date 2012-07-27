using System;
using System.IO;

namespace zVirtualClient.Storage
{
    /// <summary>
    /// IsolatedStorage device derived class
    /// </summary>
    public class IsolatedStorage : IStoreage
    {


        public IsolatedStorage() { }

        public void Save(string file, System.IO.MemoryStream contents)
        {
            using (System.IO.IsolatedStorage.IsolatedStorageFile isoStorage = GetISOStorage())
            {
                using (var writer = new System.IO.IsolatedStorage.IsolatedStorageFileStream(file, System.IO.FileMode.Create, isoStorage))
                {
                    if (contents.CanSeek && contents.Position > 0) contents.Seek(0, SeekOrigin.Begin);
                    byte[] data = contents.ToArray();
                    writer.Write(data, 0, data.Length);
                    writer.Close();
                }
            }
        }

        public System.IO.MemoryStream Load(string file)
        {
            using (System.IO.IsolatedStorage.IsolatedStorageFile isoStorage = GetISOStorage())
            {
                string[] storeNames = isoStorage.GetFileNames(file);
                if (storeNames == null || storeNames.Length == 0)
                    return null;
                else
                {
                    byte[] contents;
                    using (var reader = new System.IO.IsolatedStorage.IsolatedStorageFileStream(file, System.IO.FileMode.Open, isoStorage))
                    {
                        contents = new byte[reader.Length];
                        reader.Read(contents, 0, (int)reader.Length);
                    }
                    return new System.IO.MemoryStream(contents);

                }
            }
        }


        protected System.IO.IsolatedStorage.IsolatedStorageFile GetISOStorage()
        {
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
        }




        public bool FileExists(string file)
        {
            using (var iso = GetISOStorage())
            {
                string[] storeNames = iso.GetFileNames(file);
                if (storeNames == null || storeNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}