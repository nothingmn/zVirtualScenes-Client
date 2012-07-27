using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.HTTP
{
    public class VirtualClientHttp : IHttpClient
    {
        public VirtualClientHttp(Credentials credentials)
        {
            this.Credentials = credentials;
        }

        public event HttpDownloaded OnHttpDownloaded;
        public event HttpDownloadError OnHttpDownloadError;
        public event HttpDownloadProgress OnHttpDownloadProgress;
        public event HttpDownloadTimeout OnHttpDownloadTimeout;

        public Credentials Credentials { get; set; }
        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        public void HTTPAsString(HttpPayload Payload)
        {
            HttpClient c = new HttpClient();
            c.OnHttpDownloaded += new HttpDownloaded(c_OnHttpDownloaded);
            c.OnHttpDownloadError += new HttpDownloadError(c_OnHttpDownloadError);
            c.OnHttpDownloadProgress += new HttpDownloadProgress(c_OnHttpDownloadProgress);
            c.OnHttpDownloadTimeout += new HttpDownloadTimeout(c_OnHttpDownloadTimeout);
            if (Payload.POST)
            {
                c.POST(Payload.Url, Payload.Data, Payload.Key, Payload.Cookies, null, null, null, null, true, null, false, null);
            }
            else
            {
                c.GET(Payload.Url, Payload.Key, Payload.Cookies, null, null, null, null, true, null, false, null);
            }
        }

        void c_OnHttpDownloadTimeout(object Sender, long Duration, string Key)
        {
            if (OnHttpDownloadTimeout != null) OnHttpDownloadTimeout(Sender, Duration, Key);
        }

        void c_OnHttpDownloadProgress(object Sender, long ContentLength, long BytesSoFar, double Progress, long Duration, string Key)
        {
            if (OnHttpDownloadProgress != null) OnHttpDownloadProgress(Sender, ContentLength, BytesSoFar, Progress, Duration, Key);
        }

        void c_OnHttpDownloadError(object Sender, Exception exception, string Key)
        {
            if (OnHttpDownloadError != null) OnHttpDownloadError(Sender, exception, Key);
        }

        void c_OnHttpDownloaded(object Sender, byte[] Data, long Duration, string Key)
        {
            if (OnHttpDownloaded != null) OnHttpDownloaded(Sender, Data, Duration, Key);
        }
    }
}
