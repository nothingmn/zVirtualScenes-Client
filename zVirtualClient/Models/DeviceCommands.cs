using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class DeviceCommands
    {
        public bool success { get; set; }
        List<DeviceCommand> cmds;
        public List<DeviceCommand> DeviceCommand
        {
            get { return cmds; }
            set { cmds = value; }
        }
        public List<DeviceCommand> device_commands
        {
            get { return cmds; }
            set { cmds = value; }
        }
    }
}