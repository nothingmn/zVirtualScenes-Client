using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Interfaces
{
    public interface IServiceController
    {
        Credentials Credentials { get; set; }
        Models.LoginResponse Login();
        Models.LoginResponse Logout();
        Models.Devices Devices();
        Models.DeviceDetails DeviceDetails(int DeviceID);
        Models.DeviceCommands DeviceCommands(int DeviceID);
        Models.DeviceCommandResponse DeviceCommand(int DeviceID, string Name, int arg, string type);
        Models.DeviceValues DeviceValues(int DeviceID);
        Models.SceneResponse Scenes();
        Models.SceneNameChangeResponse ChangeSceneName(int SceneID, string Name);
        Models.SceneNameChangeResponse StartScene(int SceneID);

        Models.GroupsResponse Groups();
        Models.GroupDetailsResponse GroupDetails(int GroupID);

        Models.CommandsResponse Commands();

        Models.CommandsResponse SendCommand(Models.BuiltinCommand Command);
    }
}
