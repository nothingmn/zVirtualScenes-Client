using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using zVirtualClient.HTTP;
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
        public Credential Credential { get; set; }
        public Helpers.ILog Logger { get; set; }


        public System.Net.CookieContainer Cookies { get; set; }

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

        public event Request OnRequest;
        public event RequestCompleted OnRequestCompleted;

        private Configuration.IConfigurationReader ConfigurationReader;
        public Client(Credential Credential, IHttpClient HttpClient = null, IServiceController Controller = null, Configuration.IConfigurationReader ConfigurationReader = null)
        {
            if (ConfigurationReader == null)
            {
#if WINDOWS_PHONE
                ConfigurationReader = new Configuration.IsolatedStorageConfigurationReader();
#else
                ConfigurationReader = new Configuration.AppConfigConfigurationReader();
#endif
            }
            this.ConfigurationReader = ConfigurationReader;
            if (HttpClient == null)
            {
                if (HttpClient == null)
                    HttpClient = new HTTP.VirtualClientHttp(this.Credential);
            }
            if (Controller == null)
            {
                Controller = new VirtualScenes34.VitualScenes34Controller(Credential);
            }
            Logger = LogManager.GetLogger<Client>();
            this.Credential = Credential;
            this.VirtualScenesController = Controller;
            if (this.VirtualScenesController.Credential == null) this.VirtualScenesController.Credential = Credential;
            this.VirtualScenesController.OnError += new Error(VirtualScenesController_OnError);

            Logger.Debug("New Client Created");
            Logger.DebugFormat("HttpClient:{0}", HttpClient.ToString());
            Logger.DebugFormat("Controller:{0}", Controller.ToString());
            Logger.DebugFormat("Credential:{0}", Helpers.Serialization.NewtonSerializer<Credential>.ToJSON(Credential));

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


            LoadCookie();
        }

        public void PersistCookie()
        {
            //try
            //{
            //    var cookies = VirtualScenesController.Cookies.GetCookies(this.Credential.Uri);
            //    if (cookies != null && cookies.Count > 0)
            //    {
            //        var cookie = cookies["zvs"];
            //        if (cookie != null)
            //        {
            //            ConfigurationReader.WriteSetting<string>("cookie", cookie.Value);
            //        }
            //    }

            //}
            //catch (Exception e)
            //{
                    
            //}
        }

        public void KillCookie()
        {
            //ConfigurationReader.WriteSetting<string>("cookie", "");
        }
        public void LoadCookie()
        {
            //try
            //{
            //    var cookieGuid = ConfigurationReader.ReadSetting<string>("cookie");
            //    if (!string.IsNullOrEmpty(cookieGuid))
            //    {
            //        this.VirtualScenesController.Cookies = new CookieContainer();
            //        this.VirtualScenesController.Cookies.Add(this.Credential.Uri, new Cookie("zvs", cookieGuid));
            //        this.Cookies = this.VirtualScenesController.Cookies;
            //    }
            //}
            //catch (Exception e)
            //{
            //}
        }

        void VirtualScenesController_OnError(object Sender, string Message, Exception Exception)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(Sender, "OnError");
            if (OnError != null) OnError(Sender, Message, Exception);
        }

        public void Login()
        {
            if (OnRequest != null) OnRequest(this, "Login");
            VirtualScenesController.Login();
        }

        void VirtualScenesController_OnLogin(Models.LoginResponse LoginResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnLogin");
            if (OnLogin != null) OnLogin(LoginResponse);
        }
        public void Logout()
        {
            if (OnRequest != null) OnRequest(this, "Logout");
            VirtualScenesController.Logout();
        }

        void VirtualScenesController_OnLogout(Models.LoginResponse LoginResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnLogout");
            if (OnLogout != null) OnLogout(LoginResponse);
        }
        public void Devices()
        {
            if (OnRequest != null) OnRequest(this, "Devices");
            VirtualScenesController.Devices();
        }

        void VirtualScenesController_OnDevices(Models.Devices DevicesResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnDevices");
            if (OnDevices != null) OnDevices(DevicesResponse);
        }
        public void DeviceDetails(int DeviceID)
        {
            if (OnRequest != null) OnRequest(this, "DeviceDetails");
            VirtualScenesController.DeviceDetails(DeviceID);
        }

        void VirtualScenesController_OnDeviceDetails(Models.DeviceDetails DeviceDetailsResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnDeviceDetails");
            if (OnDeviceDetails != null) OnDeviceDetails(DeviceDetailsResponse);
        }
        public void DeviceCommands(int DeviceID)
        {
            if (OnRequest != null) OnRequest(this, "DeviceCommands");
            VirtualScenesController.DeviceCommands(DeviceID);
        }

        void VirtualScenesController_OnDeviceCommands(Models.DeviceCommands DeviceCommandsResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnDeviceCommands");
            if (OnDeviceCommands != null) OnDeviceCommands(DeviceCommandsResponse);
        }
        public void DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            if (OnRequest != null) OnRequest(this, "DeviceCommand");
            VirtualScenesController.DeviceCommand(DeviceID, Name, arg, type);
        }

        void VirtualScenesController_OnDeviceCommand(Models.DeviceCommandResponse DeviceCommandResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnDeviceCommand");
            if (OnDeviceCommand != null) OnDeviceCommand(DeviceCommandResponse);
        }
        public void DeviceValues(int DeviceID)
        {
            if (OnRequest != null) OnRequest(this, "DeviceValues");
            VirtualScenesController.DeviceValues(DeviceID);
        }

        void VirtualScenesController_OnDeviceValues(Models.DeviceValues DeviceValuesResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnDeviceValues");
            if (OnDeviceValues != null) OnDeviceValues(DeviceValuesResponse);
        }

        public void Scenes()
        {
            if (OnRequest != null) OnRequest(this, "Scenes");
            VirtualScenesController.Scenes();
        }

        void VirtualScenesController_OnScenes(Models.SceneResponse SceneResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnScenes");
            if (OnScenes != null) OnScenes(SceneResponse);
        }

        public void ChangeSceneName(int SceneID, string Name)
        {
            if (OnRequest != null) OnRequest(this, "ChangeSceneName");
            VirtualScenesController.ChangeSceneName(SceneID, Name);
        }

        void VirtualScenesController_OnChangeSceneName(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnChangeSceneName");
            if (OnChangeSceneName != null) OnChangeSceneName(SceneNameChangeResponse);
        }

        public void StartScene(int SceneID)
        {
            if (OnRequest != null) OnRequest(this, "StartScene");
            VirtualScenesController.StartScene(SceneID);
        }

        void VirtualScenesController_OnStartScene(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnStartScene");
            if (OnStartScene != null) OnStartScene(SceneNameChangeResponse);
        }

        public void Groups()
        {
            if (OnRequest != null) OnRequest(this, "Groups");
            VirtualScenesController.Groups();
        }

        void VirtualScenesController_OnGroups(Models.GroupsResponse GroupsResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnGroups");
            if (OnGroups != null) OnGroups(GroupsResponse);
        }
        public void GroupDetails(int GroupID)
        {
            if (OnRequest != null) OnRequest(this, "GroupDetails");
            VirtualScenesController.GroupDetails(GroupID);
        }

        void VirtualScenesController_OnGroupDetails(Models.GroupDetailsResponse GroupDetailsResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnGroupDetails");
            if (OnGroupDetails != null) OnGroupDetails(GroupDetailsResponse);
        }

        public void Commands()
        {
            if (OnRequest != null) OnRequest(this, "Commands");
            VirtualScenesController.Commands();
        }

        void VirtualScenesController_OnCommands(Models.CommandsResponse CommandsResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnCommands");
            if (OnCommands != null) OnCommands(CommandsResponse);

        }
        public void SendCommand(Models.BuiltinCommand Command)
        {
            if (OnRequest != null) OnRequest(this, "SendCommand");
            VirtualScenesController.SendCommand(Command);
        }

        void VirtualScenesController_OnSendCommand(Models.CommandsResponse CommandsResponse)
        {
            if (OnRequestCompleted != null) OnRequestCompleted(this, "OnSendCommand");
            if (OnSendCommand != null) OnSendCommand(CommandsResponse);
        }
    }
}