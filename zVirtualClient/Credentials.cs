using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace zVirtualClient
{

    public class Credentials
    {

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.Uri Uri { get; set; }
        public string Domain { get; set; }

        public Credentials(string Host, int Port, string Username, string Password, Configuration.IConfigurationReader ConfigurationReader = null)
        {
            if (ConfigurationReader == null)
            {
#if WINDOWS_PHONE
                ConfigurationReader = new Configuration.IsolatedStorageConfigurationReader();
#else
                ConfigurationReader = new Configuration.AppConfigConfigurationReader();
#endif
            }
            this.Host = Host;
            this.Port = Port;
            this.Username = Username;
            this.Password = Password;
            if (Port <= 0) Port = 80;
            if (ConfigurationReader != null)
            {
                if (string.IsNullOrEmpty(this.Host))
                {
                    this.Host = ConfigurationReader.ReadSetting<string>("Host");
                }
                if (this.Port <= 0)
                {
                    this.Port = ConfigurationReader.ReadSetting<int>("Port");
                }

                if (string.IsNullOrEmpty(this.Username))
                {
                    this.Username = ConfigurationReader.ReadSetting<string>("Username");
                }
                if (string.IsNullOrEmpty(this.Password))
                {
                    this.Password = ConfigurationReader.ReadSetting<string>("Password");
                }
            }

            System.Uri u;
            if (Uri.TryCreate(string.Format("{0}:{1}", Host, Port), UriKind.RelativeOrAbsolute, out u))
            {
                Uri = u;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Host and/or Port not valid for URI Creation:" + string.Format("{0}:{1}", Host, Port));
            }


        }

    }
}