using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Interfaces
{
    interface IUrlBuilder
    {        
        Credentials Credentials { get; set; }
        HttpPayload LoginPayload();
        HttpPayload LogoutPayload();
        HttpPayload DevicesPayload();
        HttpPayload DeviceDetailsPayload(int DeviceID);
        HttpPayload DeviceCommandsPayload(int DeviceID);
    }
}
