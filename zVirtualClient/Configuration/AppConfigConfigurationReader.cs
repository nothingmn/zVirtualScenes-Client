using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Configuration
{
    public class AppConfigConfigurationReader : IConfigurationReader
    {
        public T ReadSetting<T>(string Key)
        {
            try
            {
                string value = System.Configuration.ConfigurationSettings.AppSettings[Key];
                return (T) Convert.ChangeType(value, typeof (T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        public void WriteSetting<T>(string Key, T Value)
        {
            System.Configuration.ConfigurationSettings.AppSettings[Key] = (string)Convert.ChangeType(Value, typeof(string));
            //throw new NotImplementedException();
        }
    }
}
