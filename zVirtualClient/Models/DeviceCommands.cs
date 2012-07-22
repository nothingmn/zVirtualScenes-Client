using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class DeviceCommands
    {
        public bool success { get; set; }
        public List<DeviceCommand> device_commands { get; set; }
    }
}
