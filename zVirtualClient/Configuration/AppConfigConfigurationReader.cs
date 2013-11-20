using System;
using System.Collections.Generic;
using System.Configuration;
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
                if (config.AppSettings.Settings.AllKeys.Contains(Key))
                {
                    string value = config.AppSettings.Settings[Key].Value;
                    return (T) Convert.ChangeType(value, typeof (T));
                }
            }
            catch (Exception)
            {
            }
            return default(T);
        }

        System.Configuration.Configuration config =
            System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public void WriteSetting<T>(string Key, T Value)
        {
            if (config.AppSettings.Settings.AllKeys.Contains(Key))
            {
                config.AppSettings.Settings.Remove(Key);
            }            
            config.AppSettings.Settings.Add(Key, (string) Convert.ChangeType(Value, typeof (string)));
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
