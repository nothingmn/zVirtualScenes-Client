using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using zVirtualClient.Interfaces;


namespace zVirtualClient.Desktop
{
    public class HttpClient : IHttpClient
    {

        public Credentials Credentials { get; set; }        
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }

        Helpers.ILog log;
        Helpers.Serialization.ISerialize<HttpPayload> payLoadSerializer;

        public HttpClient(Credentials Credentials)
        {
            this.Credentials = Credentials;
            payLoadSerializer = new Helpers.Serialization.JSONSerializer<HttpPayload>();
            log = new Helpers.log4netLogger<HttpClient>();
        }

        public string HTTPAsString(HttpPayload Payload)
        {
            log.Debug("Request:" + payLoadSerializer.Serialize(Payload));
            string result = System.Text.Encoding.UTF8.GetString(HTTPAsBytes(Payload));
            log.Debug("Response:" + result);
            return result;
        }

        private byte[] HTTPAsBytes(HttpPayload Payload)
        {
            WebResponse res = HTTPAsWebResponse(Payload);
            return ConvertWebResponseToByteArray(res);
        }

        public WebResponse HTTPAsWebResponse(HttpPayload Payload)
        {

            if (!Payload.POST && Payload.Data != null && Payload.Data.Length > 0)
            {
                string restoftheurl = System.Text.ASCIIEncoding.ASCII.GetString(Payload.Data);
                if (Payload.Url.IndexOf("?") <= 0)
                    Payload.Url = Payload.Url + "?";

                Payload.Url = Payload.Url + restoftheurl;
            }

            System.Net.WebRequest wreq = System.Net.HttpWebRequest.Create(Payload.Url);
            wreq.Method = "GET";
            if (Payload.POST)
                wreq.Method = "POST";

            if (ProxyAddress != null && ProxyAddress.Trim() != string.Empty && ProxyPort > 0)
            {
                WebProxy webProxy = new WebProxy(ProxyAddress, ProxyPort);
                webProxy.BypassProxyOnLocal = true;
                wreq.Proxy = webProxy;
            }
            else
            {
                // wreq.Proxy = WebProxy.GetDefaultProxy();
                wreq.Proxy = WebRequest.DefaultWebProxy;
            }

            if(!string.IsNullOrEmpty(Credentials.Username) && !string.IsNullOrEmpty(Credentials.Domain) && !string.IsNullOrEmpty(Credentials.Password))
                wreq.Credentials = new NetworkCredential(Credentials.Username, Credentials.Password, Credentials.Domain);
            else if (!string.IsNullOrEmpty(Credentials.Username) && !string.IsNullOrEmpty(Credentials.Password))
                wreq.Credentials = new NetworkCredential(Credentials.Username, Credentials.Password);

            if (Payload.Data != null)
                wreq.ContentLength = Payload.Data.Length;
            else
                wreq.ContentLength = 0;


            if (Payload.Cookies != null)
            {
                HttpWebRequest w = (wreq as HttpWebRequest);
                w.CookieContainer = Payload.Cookies;
            }

            if (Payload.POST && Payload.Data != null && Payload.Data.Length > 0)
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
                using (Stream request = wreq.GetRequestStream())
                {
                    request.Write(Payload.Data, 0, Payload.Data.Length);
                }
            }

            WebResponse wrsp = wreq.GetResponse();
            return wrsp;
        }

        /// <summary>
        /// its a default of 1024 bytes for the buffer because of download speeds and such
        /// some arbritray number which to buffer the downloaded content
        /// too high of a number and the responsestream cant keep up
        /// too low and the more loops, and ore time it takes to get the content
        /// if your constantly tearing the same URL down you may want to test 
        /// with different buffersizes for optimal performance
        /// with my tests 1024 (1kb/s) was optimal for text and binary data
        /// </summary>
        private static int BufferSize = 1024;

        public static byte[] ConvertWebResponseToByteArray(WebResponse res)
        {
            byte[] final = null;
            using (BinaryReader br = new BinaryReader(res.GetResponseStream()))
            {

                // Download and buffer the binary stream into a memory stream
                using (MemoryStream stm = new MemoryStream())
                {
                    int pos = 0;
                    int maxread = BufferSize;
                    while (true)
                    {
                        byte[] content = br.ReadBytes(maxread);
                        if (content.Length <= 0)
                            break;

                        if (content.Length < maxread)
                            maxread = maxread - content.Length;

                        stm.Write(content, 0, content.Length);
                        pos += content.Length;
                    }
                    stm.Position = 0;
                    final = new byte[(int)stm.Length];
                    stm.Read(final, 0, final.Length);

                }
            }
            return final;
        }

    }
}
