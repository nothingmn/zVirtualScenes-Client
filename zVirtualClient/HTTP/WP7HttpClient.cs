using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace zVirtualClient.HTTP
{
    public class WP7HttpClient : Interfaces.IHttpClient
    {
        public WP7HttpClient(Credentials Credentials)
        {
            this.Credentials = Credentials;
        }
        public Credentials Credentials { get; set; }

        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }

        ManualResetEvent waiter = new ManualResetEvent(false);
        public string HTTPAsString(Interfaces.HttpPayload Payload)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Begin), Payload);
            WaitHandle.WaitAll(new ManualResetEvent[] { waiter });
            return results;
        }
        private string results = "";
        private void Begin(object State)
        {
            Interfaces.HttpPayload Payload = (Interfaces.HttpPayload)State;
            HttpWebRequest request = HttpWebRequest.CreateHttp(Payload.Url);
            if (Payload.Cookies != null) request.CookieContainer = Payload.Cookies;

            request.AllowAutoRedirect = true;
            request.BeginGetResponse(new AsyncCallback(HandleResponse), request);
        }

        public void HandleResponse(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;

            if (request != null)
            {
                using (WebResponse response = request.EndGetResponse(result))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        results = reader.ReadToEnd();
                        waiter.Set();
                        // do something with the feed here
                    }
                }
            }
        }
    }
}