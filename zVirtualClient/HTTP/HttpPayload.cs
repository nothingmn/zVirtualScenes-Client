using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Runtime.Serialization;

namespace zVirtualClient.HTTP
{
    
    public class HttpPayload
    {
        public string Key { get; set; }
        public string Url { get; set; }
        public bool POST { get; set; }
        public byte[] Data { get; set; }
        public WebHeaderCollection Headers { get; set; }

         public HttpPayload()
         {
             Headers = new WebHeaderCollection();
             Cookies = new CookieContainer();
         }

        public string RawData
        {
            get
            {
                if (Data != null && Data.Length > 0)
                {
#if WINDOWS_PHONE
                    return System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length);
#else
                    return System.Text.Encoding.UTF8.GetString(Data);
#endif
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Data = System.Text.Encoding.UTF8.GetBytes(value);
                else
                    Data = null;
            }
        }
        //[IgnoreDataMember]
        public System.Net.CookieContainer Cookies { get; set; }

    }
}
