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

        public Credentials(string Host, int Port, string Username, string Password)
        {
            this.Host = Host;
            this.Port = Port;
            this.Username = Username;
            this.Password = Password;
            if (Port <= 0) Port = 80;
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