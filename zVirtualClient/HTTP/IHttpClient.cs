using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.HTTP;

namespace zVirtualClient.HTTP
{
    public interface IHttpClient
    {
        Credentials Credentials { get; set; }
        string ProxyAddress { get; set; }
        int ProxyPort { get; set; }
        void HTTPAsString(HttpPayload Payload);
        event HttpDownloaded OnHttpDownloaded;
        event HttpDownloadError OnHttpDownloadError;
        event HttpDownloadProgress OnHttpDownloadProgress;
        event HttpDownloadTimeout OnHttpDownloadTimeout;

    }
}