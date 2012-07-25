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

            VirtualScenesController.OnLogin += new LoginResponse(VirtualScenesController_OnLogin);
            VirtualScenesController.OnLogout += new LogoutResponse(VirtualScenesController_OnLogout);
            VirtualScenesController.OnDevices += new DevicesResponse(VirtualScenesController_OnDevices);
            VirtualScenesController.OnDeviceDetails += new DeviceDetailsResponse(VirtualScenesController_OnDeviceDetails);
            VirtualScenesController.OnDeviceCommands += new DeviceCommandsResponse(VirtualScenesController_OnDeviceCommands);
            VirtualScenesController.OnDeviceCommand += new DeviceCommandResponse(VirtualScenesController_OnDeviceCommand);
            VirtualScenesController.OnDeviceValues += new DeviceValuesResponse(VirtualScenesController_OnDeviceValues);
            VirtualScenesController.OnScenes += new SceneResponse(VirtualScenesController_OnScenes);
            VirtualScenesController.OnChangeSceneName += new SceneNameChangeResponse(VirtualScenesController_OnChangeSceneName);
            VirtualScenesController.OnStartScene += new SceneNameChangeResponse(VirtualScenesController_OnStartScene);
            VirtualScenesController.OnGroups += new GroupsResponse(VirtualScenesController_OnGroups);
            VirtualScenesController.OnGroupDetails += new GroupDetailsResponse(VirtualScenesController_OnGroupDetails);
            VirtualScenesController.OnCommands += new CommandsResponse(VirtualScenesController_OnCommands);
            VirtualScenesController.OnSendCommand += new CommandsResponse(VirtualScenesController_OnSendCommand);

        }

        void VirtualScenesController_OnError(object Sender, string Message, Exception Exception)
        {
            if (OnError != null) OnError(Sender, Message, Exception);
        }

        public void Login()
        {
            VirtualScenesController.Login();
        }

        void VirtualScenesController_OnLogin(Models.LoginResponse LoginResponse)
        {
            if (OnLogin != null) OnLogin(LoginResponse);
        }
        public void Logout()
        {
            VirtualScenesController.Logout();
        }

        void VirtualScenesController_OnLogout(Models.LoginResponse LoginResponse)
        {
            if (OnLogout != null) OnLogout(LoginResponse);
        }
        public void Devices()
        {
            VirtualScenesController.Devices();
        }

        void VirtualScenesController_OnDevices(Models.Devices DevicesResponse)
        {
            if (OnDevices != null) OnDevices(DevicesResponse);
        }
        public void DeviceDetails(int DeviceID)
        {
            VirtualScenesController.DeviceDetails(DeviceID);
        }

        void VirtualScenesController_OnDeviceDetails(Models.DeviceDetails DeviceDetailsResponse)
        {
            if (OnDeviceDetails != null) OnDeviceDetails(DeviceDetailsResponse);
        }
        public void DeviceCommands(int DeviceID)
        {
            VirtualScenesController.DeviceCommands(DeviceID);
        }

        void VirtualScenesController_OnDeviceCommands(Models.DeviceCommands DeviceCommandsResponse)
        {
            if (OnDeviceCommands != null) OnDeviceCommands(DeviceCommandsResponse);
        }
        public void DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            VirtualScenesController.DeviceCommand(DeviceID, Name, arg, type);
        }

        void VirtualScenesController_OnDeviceCommand(Models.DeviceCommandResponse DeviceCommandResponse)
        {
            if (OnDeviceCommand != null) OnDeviceCommand(DeviceCommandResponse);
        }
        public void DeviceValues(int DeviceID)
        {
            VirtualScenesController.DeviceValues(DeviceID);
        }

        void VirtualScenesController_OnDeviceValues(Models.DeviceValues DeviceValuesResponse)
        {
            if (OnDeviceValues != null) OnDeviceValues(DeviceValuesResponse);
        }

        public void Scenes()
        {
            VirtualScenesController.Scenes();
        }

        void VirtualScenesController_OnScenes(Models.SceneResponse SceneResponse)
        {
            if (OnScenes != null) OnScenes(SceneResponse);
        }

        public void ChangeSceneName(int SceneID, string Name)
        {
            VirtualScenesController.ChangeSceneName(SceneID, Name);
        }

        void VirtualScenesController_OnChangeSceneName(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            if (OnChangeSceneName != null) OnChangeSceneName(SceneNameChangeResponse);
        }

        public void StartScene(int SceneID)
        {
            VirtualScenesController.StartScene(SceneID);
        }

        void VirtualScenesController_OnStartScene(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            if (OnStartScene != null) OnStartScene(SceneNameChangeResponse);
        }

        public void Groups()
        {
            VirtualScenesController.Groups();
        }

        void VirtualScenesController_OnGroups(Models.GroupsResponse GroupsResponse)
        {
            if (OnGroups != null) OnGroups(GroupsResponse);
        }
        public void GroupDetails(int GroupID)
        {
            VirtualScenesController.GroupDetails(GroupID);
        }

        void VirtualScenesController_OnGroupDetails(Models.GroupDetailsResponse GroupDetailsResponse)
        {
            if (OnGroupDetails != null) OnGroupDetails(GroupDetailsResponse);
        }

        public void Commands()
        {
            VirtualScenesController.Commands();
        }

        void VirtualScenesController_OnCommands(Models.CommandsResponse CommandsResponse)
        {
            if (OnCommands != null) OnCommands(CommandsResponse);

        }
        public void SendCommand(Models.BuiltinCommand Command)
        {
            VirtualScenesController.SendCommand(Command);
        }

        void VirtualScenesController_OnSendCommand(Models.CommandsResponse CommandsResponse)
        {
            if (OnSendCommand != null) OnSendCommand(CommandsResponse);
        }
    }
}