using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vcmd
{
    public enum Actions {
        ListDevices,
        DeviceDetails,
        ListDeviceCommands,
        DeviceCommand,
        ListDeviceValues,
        ListScenes,
        ChangeSceneName,
        StartScene,
        ListGroups,
        ListGroupsDetails,
        ListBuiltInCommands,
        SendBuiltInCommand
    }
    public class Arguments
    {
        [Argument(ArgumentType.AtMostOnce, ShortName = "pa", HelpText = "Password to the zVirtualScenes Server")]
        public string Password { get; set; }

        [Argument(ArgumentType.AtMostOnce, ShortName = "h", HelpText = "Host to the zVirtualScenes Serer (http://server.com)")]
        public string Host { get; set; }

        [Argument(ArgumentType.AtMostOnce, ShortName = "po", HelpText = "Port to the zVirtualScenes Serer")]
        public int Port { get; set; }

        [Argument(ArgumentType.Required, ShortName = "a", HelpText = "Action to take")]
        public Actions Action { get; set; }

        [Argument(ArgumentType.AtMostOnce, ShortName="d", HelpText = "Device to take the action on")]
        public int DeviceID { get; set; }

        [Argument(ArgumentType.AtMostOnce, ShortName="s", HelpText = "Scene to take the action on")]
        public int SceneID { get; set; }

         [Argument(ArgumentType.AtMostOnce, ShortName="g", HelpText = "Group to take the action on")]
        public int GroupID { get; set; }

        [Argument(ArgumentType.AtMostOnce, ShortName="n", HelpText = "Name parameter")]
         public string Name { get; set; }

        [Argument(ArgumentType.AtMostOnce,  HelpText = "Argument to pass")]
        public int arg { get; set; }

        [Argument(ArgumentType.AtMostOnce, ShortName="t", HelpText = "Type of command")]
        public string type { get; set; }

    }
}
