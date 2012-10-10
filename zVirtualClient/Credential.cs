using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace zVirtualClient
{

    public class Credential
    {
        public bool Default { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public System.Uri Uri
        {
            get
            {
                if(!Host.ToLower().StartsWith("http"))
                {
                    Host = "http://" + Host;
                }
                System.Uri u;
                if (Uri.TryCreate(string.Format("{0}:{1}", Host, Port), UriKind.RelativeOrAbsolute, out u))
                {
                    return u;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Host and/or Port not valid for URI Creation:" + string.Format("{0}:{1}", Host, Port));
                }

            }
        }
        public override string ToString()
        {
            return Name;
        }

    }
}