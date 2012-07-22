using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace zVirtualClient.Interfaces
{
    [DataContract]
    public class HttpPayload
    {
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public bool POST { get; set; }
        [DataMember]
        public byte[] Data { get; set; }
        [DataMember]
        public string RawData
        {
            get
            {
                if (Data != null && Data.Length > 0)
                    return System.Text.Encoding.UTF8.GetString(Data);
                else
                    return "";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data = System.Text.Encoding.UTF8.GetBytes(value);
                else
                    Data = null;
            }
        }
        [IgnoreDataMember]
        public System.Net.CookieContainer Cookies { get; set; }

    }
}
