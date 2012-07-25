using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.Interfaces;
using zVirtualClient.HTTP;


namespace zVirtualClient.VirtualScenes34
{
    public class VitualScenes34Controller : IServiceController
    {
        Helpers.ILog log;
        System.Net.CookieContainer Cookies = new System.Net.CookieContainer();
        IUrlBuilder UrlBuilder;
        public Credentials Credentials { get; set; }
        IHttpClient HttpClient;

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
     

        public VitualScenes34Controller(Credentials Credentials, IHttpClient HttpClient = null)
        {
            log = Client.LogManager.GetLogger<VitualScenes34Controller>();

            this.Credentials = Credentials;
            this.UrlBuilder = new VirtualScenes34UrlBuilder(this.Credentials);

            if (HttpClient == null)
                HttpClient = new HTTP.VirtualClientHttp(this.Credentials);

            this.HttpClient = HttpClient;

            this.HttpClient.OnHttpDownloadError += new HttpDownloadError(HttpClient_OnHttpDownloadError);
            this.HttpClient.OnHttpDownloadTimeout += new HttpDownloadTimeout(HttpClient_OnHttpDownloadTimeout);
        }

        void HttpClient_OnHttpDownloadTimeout(object Sender, long Duration, string Key)
        {
            if (this.OnError != null) this.OnError(Sender, "Request Timed Out", null);
        }

        void HttpClient_OnHttpDownloadError(object Sender, Exception exception, string Key)
        {
            if (this.OnError != null) this.OnError(Sender, Key, exception);
        }


        Helpers.Serialization.ISerialize<Models.LoginResponse> loginSerializer = new Helpers.Serialization.NewtonSerializer<Models.LoginResponse>();
            
        public void Login()
        {
            HttpPayload login = this.UrlBuilder.LoginPayload();          
            login.Cookies = this.Cookies;
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloadedLogin);
            HttpClient.HTTPAsString(login);                        
        }

        void HttpClient_OnHttpDownloadedLogin(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloadedLogin);
            var result = loginSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnLogin != null) OnLogin(result);
        }

        public void Logout()
        {
            HttpPayload logout = this.UrlBuilder.LogoutPayload();
            logout.Cookies = this.Cookies;
            HttpClient.HTTPAsString(logout);
            //return loginSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.Devices> devicesSerializer = new Helpers.Serialization.NewtonSerializer<Models.Devices>();
        public void Devices()
        {
            HttpPayload devices = this.UrlBuilder.DevicesPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return devicesSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceDetails> deviceDetailsSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceDetails>();
        public void DeviceDetails(int DeviceID)
        {
            HttpPayload devices = this.UrlBuilder.DeviceDetailsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return deviceDetailsSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceCommands> deviceCommandsSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceCommands>();
        public void DeviceCommands(int DeviceID)
        {
            HttpPayload devices = this.UrlBuilder.DeviceCommandsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return deviceCommandsSerializer.Deserialize(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceCommandResponse> deviceCommandsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceCommandResponse>();
        public void DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            HttpPayload devices = this.UrlBuilder.DeviceCommandPayload(DeviceID, Name, arg, type);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return deviceCommandsResponseSerializer.Deserialize(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceValues> deviceValuesResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceValues>();
        public void DeviceValues(int DeviceID)
        {
            HttpPayload devices = this.UrlBuilder.DeviceValuesPayload(DeviceID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return deviceValuesResponseSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.SceneResponse> SceneResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.SceneResponse>();
        public void Scenes()
        {
            HttpPayload devices = this.UrlBuilder.ScenesPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return SceneResponseSerializer.Deserialize(result);
        }


        Helpers.Serialization.ISerialize<Models.SceneNameChangeResponse> SceneNameChangeResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.SceneNameChangeResponse>();
        public void ChangeSceneName(int SceneID, string Name)
        {
            HttpPayload devices = this.UrlBuilder.ScenesChangeNamePayload(SceneID, Name);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return SceneNameChangeResponseSerializer.Deserialize(result);
        }

        public void StartScene(int SceneID)
        {
            HttpPayload devices = this.UrlBuilder.StartScenePayload(SceneID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

            //return SceneNameChangeResponseSerializer.Deserialize(result);

        }

        Helpers.Serialization.ISerialize<Models.GroupsResponse> GroupsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.GroupsResponse>();
        public void Groups()
        {
            HttpPayload devices = this.UrlBuilder.GroupsPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

            //return GroupsResponseSerializer.Deserialize(result);

        }


        Helpers.Serialization.ISerialize<Models.GroupDetailsResponse> GroupDetailsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.GroupDetailsResponse>();
        public void GroupDetails(int GroupID)
        {
            HttpPayload devices = this.UrlBuilder.GroupDetailsPayload(GroupID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return GroupDetailsResponseSerializer.Deserialize(result);

        }

        Helpers.Serialization.ISerialize<Models.CommandsResponse> CommandsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.CommandsResponse>();
        public void Commands()
        {
            HttpPayload devices = this.UrlBuilder.CommandsPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return CommandsResponseSerializer.Deserialize(result);

        }

        public void SendCommand(Models.BuiltinCommand Command)
        {
            HttpPayload devices = this.UrlBuilder.SendCommandsPayload(Command);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
            //return CommandsResponseSerializer.Deserialize(result);
        }
        
    } 
}
