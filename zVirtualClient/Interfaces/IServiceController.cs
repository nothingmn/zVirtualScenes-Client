using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Interfaces
{
    public interface IServiceController
    {
        Credentials Credentials { get; set; }
        Models.LoginResult Login();
        Models.LoginResult Logout();
        Models.Devices Devices();
        Models.DeviceDetails DeviceDetails(int DeviceID);
    }
}
