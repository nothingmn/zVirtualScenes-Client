using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.Interfaces;

namespace zVirtualClient
{
    public class Client : IServiceController
    {
        private static object _lock = new object();
        public static Helpers.ILogManager LogManager
        {
            get
            {
                lock (_lock)
                {
                    if (lm != null) return lm;
                    lm = new Helpers.LogManager();
                    lm.ConfigureLogging();
                }
                return lm;
            }
        }
        static Helpers.ILogManager lm;

        IHttpClient HttpClient { get; set; }
        IServiceController VirtualScenesController { get; set; }
        public Credentials Credentials { get; set; }
        public Helpers.ILog Logger { get; set; }



        public event LoginResponse OnLogin;
        public event LogoutResponse OnLogout;
        public event DevicesResponse OnDevices;
        public event DeviceDetailsResponse OnDeviceDetails;
        public event DeviceCommandsResponse OnDeviceCommands;
        public event DeviceCommandResponse OnDeviceCommand;
        public event DeviceValuesResponse OnDeviceValues;
        public event SceneResponse OnScenes;
        public event SceneNameChangeResponse OnChangeSceneName;
        public event SceneNameChangeResponse OnStartScene;
        public event GroupsResponse OnGroups;
        public event GroupDetailsResponse OnGroupDetails;
        public event CommandsResponse OnCommands;
        public event CommandsResponse OnSendCommand;
        public event Error OnError;

        public Client(Credentials Credentials, IHttpClient HttpClient = null, IServiceController Controller = null)
        {
            if (HttpClient == null)
            {
                if (HttpClient == null)
                    HttpClient = new HTTP.VirtualClientHttp(this.Credentials);
            }
            if (Controller == null)
            {
                Controller = new VirtualScenes34.VitualScenes34Controller(Credentials);
            }
            Logger = LogManager.GetLogger<Client>();

            this.VirtualScenesController = Controller;
            if (this.VirtualScenesController.Credentials == null) this.VirtualScenesController.Credentials = Credentials;
            this.VirtualScenesController.OnError += new Error(VirtualScenesController_OnError);

            Logger.Debug("New Client Created");
            Logger.DebugFormat("HttpClient:{0}", HttpClient.ToString());
            Logger.DebugFormat("Controller:{0}", Controller.ToString());
            Logger.DebugFormat("Credentials:{0}", Helpers.Serialization.NewtonSerializer<Credentials>.ToJSON(Credentials));


        }

        void VirtualScenesController_OnError(object Sender, string Message, Exception Exception)
        {
            if (OnError != null) OnError(Sender, Message, Exception);
        }

        public void Login()
        {
            VirtualScenesController.OnLogin += new LoginResponse(VirtualScenesController_OnLogin);
            VirtualScenesController.Login();
        }

        void VirtualScenesController_OnLogin(Models.LoginResponse LoginResponse)
        {
            VirtualScenesController.OnLogin -= new LoginResponse(VirtualScenesController_OnLogin);
            if (OnLogin != null) OnLogin(LoginResponse);
        }
        public void Logout()
        {
            VirtualScenesController.OnLogout += new LogoutResponse(VirtualScenesController_OnLogout);
            VirtualScenesController.Logout();
        }

        void VirtualScenesController_OnLogout(Models.LoginResponse LoginResponse)
        {
            VirtualScenesController.OnLogout -= new LogoutResponse(VirtualScenesController_OnLogout);
            if (OnLogout != null) OnLogout(LoginResponse);
        }
        public void Devices()
        {
            VirtualScenesController.OnDevices += new DevicesResponse(VirtualScenesController_OnDevices);
            VirtualScenesController.Devices();
        }

        void VirtualScenesController_OnDevices(Models.Devices DevicesResponse)
        {
            VirtualScenesController.OnDevices -= new DevicesResponse(VirtualScenesController_OnDevices);
            if (OnDevices != null) OnDevices(DevicesResponse);
        }
        public void DeviceDetails(int DeviceID)
        {
            VirtualScenesController.OnDeviceDetails += new DeviceDetailsResponse(VirtualScenesController_OnDeviceDetails);
            VirtualScenesController.DeviceDetails(DeviceID);
        }

        void VirtualScenesController_OnDeviceDetails(Models.DeviceDetails DeviceDetailsResponse)
        {
            VirtualScenesController.OnDeviceDetails -= new DeviceDetailsResponse(VirtualScenesController_OnDeviceDetails);
            if (OnDeviceDetails != null) OnDeviceDetails(DeviceDetailsResponse);
        }
        public void DeviceCommands(int DeviceID)
        {
            VirtualScenesController.OnDeviceCommands += new DeviceCommandsResponse(VirtualScenesController_OnDeviceCommands);
            VirtualScenesController.DeviceCommands(DeviceID);
        }

        void VirtualScenesController_OnDeviceCommands(Models.DeviceCommands DeviceCommandsResponse)
        {
            VirtualScenesController.OnDeviceCommands -= new DeviceCommandsResponse(VirtualScenesController_OnDeviceCommands);
            if (OnDeviceCommands != null) OnDeviceCommands(DeviceCommandsResponse);
        }
        public void DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            VirtualScenesController.OnDeviceCommand += new DeviceCommandResponse(VirtualScenesController_OnDeviceCommand);
            VirtualScenesController.DeviceCommand(DeviceID, Name, arg, type);
        }

        void VirtualScenesController_OnDeviceCommand(Models.DeviceCommandResponse DeviceCommandResponse)
        {
            VirtualScenesController.OnDeviceCommand -= new DeviceCommandResponse(VirtualScenesController_OnDeviceCommand);
            if (OnDeviceCommand != null) OnDeviceCommand(DeviceCommandResponse);
        }
        public void DeviceValues(int DeviceID)
        {
            VirtualScenesController.OnDeviceValues += new DeviceValuesResponse(VirtualScenesController_OnDeviceValues);
            VirtualScenesController.DeviceValues(DeviceID);
        }

        void VirtualScenesController_OnDeviceValues(Models.DeviceValues DeviceValuesResponse)
        {
            VirtualScenesController.OnDeviceValues -= new DeviceValuesResponse(VirtualScenesController_OnDeviceValues);
            if (OnDeviceValues != null) OnDeviceValues(DeviceValuesResponse);
        }

        public void Scenes()
        {
            VirtualScenesController.OnScenes += new SceneResponse(VirtualScenesController_OnScenes);
            VirtualScenesController.Scenes();
        }

        void VirtualScenesController_OnScenes(Models.SceneResponse SceneResponse)
        {
            VirtualScenesController.OnScenes -= new SceneResponse(VirtualScenesController_OnScenes);
            if (OnScenes != null) OnScenes(SceneResponse);
        }

        public void ChangeSceneName(int SceneID, string Name)
        {
            VirtualScenesController.OnChangeSceneName += new SceneNameChangeResponse(VirtualScenesController_OnChangeSceneName);
            VirtualScenesController.ChangeSceneName(SceneID, Name);
        }

        void VirtualScenesController_OnChangeSceneName(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            VirtualScenesController.OnChangeSceneName -= new SceneNameChangeResponse(VirtualScenesController_OnChangeSceneName);
            if (OnChangeSceneName != null) OnChangeSceneName(SceneNameChangeResponse);
        }

        public void StartScene(int SceneID)
        {
            VirtualScenesController.OnStartScene += new SceneNameChangeResponse(VirtualScenesController_OnStartScene);
            VirtualScenesController.StartScene(SceneID);
        }

        void VirtualScenesController_OnStartScene(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            VirtualScenesController.OnStartScene -= new SceneNameChangeResponse(VirtualScenesController_OnStartScene);
            if (OnStartScene != null) OnStartScene(SceneNameChangeResponse);
        }

        public void Groups()
        {
            VirtualScenesController.OnGroups += new GroupsResponse(VirtualScenesController_OnGroups);
            VirtualScenesController.Groups();
        }

        void VirtualScenesController_OnGroups(Models.GroupsResponse GroupsResponse)
        {
            VirtualScenesController.OnGroups -= new GroupsResponse(VirtualScenesController_OnGroups);
            if (OnGroups != null) OnGroups(GroupsResponse);
        }
        public void GroupDetails(int GroupID)
        {
            VirtualScenesController.OnGroupDetails += new GroupDetailsResponse(VirtualScenesController_OnGroupDetails);
            VirtualScenesController.GroupDetails(GroupID);
        }

        void VirtualScenesController_OnGroupDetails(Models.GroupDetailsResponse GroupDetailsResponse)
        {
            VirtualScenesController.OnGroupDetails -= new GroupDetailsResponse(VirtualScenesController_OnGroupDetails);
            if (OnGroupDetails != null) OnGroupDetails(GroupDetailsResponse);
        }

        public void Commands()
        {
            VirtualScenesController.OnCommands += new CommandsResponse(VirtualScenesController_OnCommands);
            VirtualScenesController.Commands();
        }

        void VirtualScenesController_OnCommands(Models.CommandsResponse CommandsResponse)
        {
            VirtualScenesController.OnCommands -= new CommandsResponse(VirtualScenesController_OnCommands);
            if (OnCommands != null) OnCommands(CommandsResponse);

        }
        public void SendCommand(Models.BuiltinCommand Command)
        {
            VirtualScenesController.OnSendCommand += new CommandsResponse(VirtualScenesController_OnSendCommand);
            VirtualScenesController.SendCommand(Command);
        }

        void VirtualScenesController_OnSendCommand(Models.CommandsResponse CommandsResponse)
        {
            VirtualScenesController.OnSendCommand -= new CommandsResponse(VirtualScenesController_OnSendCommand);
            if (OnSendCommand != null) OnSendCommand(CommandsResponse);
        }
    }
}