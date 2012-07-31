using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Interfaces
{
    public delegate void LoginResponse(Models.LoginResponse LoginResponse);
    public delegate void LogoutResponse(Models.LoginResponse LoginResponse);
    public delegate void DevicesResponse(Models.Devices DevicesResponse);
    public delegate void DeviceDetailsResponse(Models.DeviceDetails DeviceDetailsResponse);
    public delegate void DeviceCommandsResponse(Models.DeviceCommands DeviceCommandsResponse);
    public delegate void DeviceCommandResponse(Models.DeviceCommandResponse DeviceCommandResponse);
    public delegate void DeviceValuesResponse(Models.DeviceValues DeviceValuesResponse);
    public delegate void SceneResponse(Models.SceneResponse SceneResponse);
    public delegate void SceneNameChangeResponse(Models.SceneNameChangeResponse SceneNameChangeResponse);
    public delegate void StartSceneResponse(Models.SceneNameChangeResponse StartSceneResponse);
    public delegate void GroupsResponse(Models.GroupsResponse GroupsResponse);
    public delegate void GroupDetailsResponse(Models.GroupDetailsResponse GroupDetailsResponse);
    public delegate void CommandsResponse(Models.CommandsResponse CommandsResponse);
    public delegate void SendCommandResponse(Models.BuiltinCommand BuiltinCommandResponse);
    public delegate void Error(object Sender, string Message, Exception Exception);

    public delegate void Request(object Sender, string Type);
    public delegate void RequestCompleted(object Sender, string Type);

    public interface IServiceController
    {
        Credential Credential { get; set; }
        void Login();
        void Logout();
        void Devices();
        void DeviceDetails(int DeviceID);
        void DeviceCommands(int DeviceID);
        void DeviceCommand(int DeviceID, string Name, int arg, string type);
        void DeviceValues(int DeviceID);
        void Scenes();
        void ChangeSceneName(int SceneID, string Name);
        void StartScene(int SceneID);
        void Groups();
        void GroupDetails(int GroupID);
        void Commands();
        void SendCommand(Models.BuiltinCommand Command);

        event LoginResponse OnLogin;
        event LogoutResponse OnLogout;
        event DevicesResponse OnDevices;
        event DeviceDetailsResponse OnDeviceDetails;
        event DeviceCommandsResponse OnDeviceCommands;
        event DeviceCommandResponse OnDeviceCommand;
        event DeviceValuesResponse OnDeviceValues;
        event SceneResponse OnScenes;
        event SceneNameChangeResponse OnChangeSceneName;
        event SceneNameChangeResponse OnStartScene;
        event GroupsResponse OnGroups;
        event GroupDetailsResponse OnGroupDetails;
        event CommandsResponse OnCommands;
        event CommandsResponse OnSendCommand;
        event Error OnError;

    }
}
