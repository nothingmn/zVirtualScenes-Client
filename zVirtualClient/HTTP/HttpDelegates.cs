using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace zVirtualClient.HTTP
{
    public delegate void HttpDownloaded(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection Headers = null );
    public delegate void HttpDownloadError(object Sender, System.Exception exception, string Key);
    public delegate void HttpDownloadProgress(object Sender, long ContentLength, long BytesSoFar, double Progress, long Duration, string Key);
    public delegate void HttpDownloadTimeout(object Sender, long Duration, string Key);
}
