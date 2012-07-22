using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Interfaces
{
    public interface IHttpClient
    {
        Credentials Credentials { get; set; }
        string ProxyAddress { get; set; }
        int ProxyPort { get; set; }
        string HTTPAsString(HttpPayload Payload);
        
        
    }
}