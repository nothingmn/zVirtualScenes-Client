using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using zVirtualClient.Storage;

namespace zVirtualClient.Configuration
{
    public class IsolatedStorageConfigurationReader : IConfigurationReader
    {
        Storage.IsolatedStorage iso = new IsolatedStorage();
        public T ReadSetting<T>(string Key)
        {
            try
            {
                if (iso.FileExists(Key + ".setting"))
                {
                    using (System.IO.MemoryStream stm = iso.Load(Key + ".setting"))
                    {
                        if (stm.CanSeek && stm.Position > 0) stm.Seek(0, SeekOrigin.Begin);
                        byte[] data = new byte[stm.Length];
                        stm.Read(data, 0, data.Length);
                        string value = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                        return (T) Convert.ChangeType(value, typeof (T), null);

                    }
                } else
                {
                    return default(T);
                }
            }
            catch (Exception)
            {
                return default(T);
            }

        }


        public void WriteSetting<T>(string Key, T Value)
        {
            string sValue = (string)Convert.ChangeType(Value, typeof(string), null);
            using (var mem = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sValue)))
            {
                iso.Save(Key + ".setting", mem);

            }
        }
    }
}