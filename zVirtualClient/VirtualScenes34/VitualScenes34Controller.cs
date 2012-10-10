using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using zVirtualClient.Interfaces;
using zVirtualClient.HTTP;


namespace zVirtualClient.VirtualScenes34
{
    public class VitualScenes34Controller : IServiceController
    {
        Helpers.ILog log;
        public  System.Net.CookieContainer Cookies { get; set; }
        public WebHeaderCollection Headers { get; set; }

        IUrlBuilder UrlBuilder;
        public Credential Credential { get; set; }
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
     

        public VitualScenes34Controller(Credential Credential, IHttpClient HttpClient = null)
        {
            Cookies = new System.Net.CookieContainer();
            Headers = new WebHeaderCollection();
            log = Client.LogManager.GetLogger<VitualScenes34Controller>();

            this.Credential = Credential;
            this.UrlBuilder = new VirtualScenes34UrlBuilder(this.Credential);

            if (HttpClient == null)
                HttpClient = new HTTP.VirtualClientHttp(this.Credential);

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

        private string tokenKey = "zvstoken";
        Helpers.Serialization.ISerialize<Models.LoginResponse> loginSerializer = new Helpers.Serialization.NewtonSerializer<Models.LoginResponse>();
            
        public void Login()
        {
            HttpPayload login = this.UrlBuilder.LoginPayload();
            if (this.Headers.AllKeys.Contains(tokenKey)) login.Headers[tokenKey] = this.Headers[tokenKey];
            login.Cookies = this.Cookies;
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloadedLogin);
            HttpClient.HTTPAsString(login);                        
        }

        void HttpClient_OnHttpDownloadedLogin(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloadedLogin);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = loginSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));            
            if (OnLogin != null) OnLogin(result);
        }

        public void Logout()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_logout);
            HttpPayload logout = this.UrlBuilder.LogoutPayload();
            if (this.Headers.AllKeys.Contains(tokenKey)) logout.Headers[tokenKey] = this.Headers[tokenKey];
            logout.Cookies = this.Cookies;
            HttpClient.HTTPAsString(logout);
            
        }

        void HttpClient_OnHttpDownloaded_logout(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_logout);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = loginSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnLogout != null) OnLogout(result);
        }

        Helpers.Serialization.ISerialize<Models.Devices> devicesSerializer = new Helpers.Serialization.NewtonSerializer<Models.Devices>();
        public void Devices()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devices);
            HttpPayload devices = this.UrlBuilder.DevicesPayload();
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devices(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devices);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = devicesSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDevices != null) OnDevices(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceDetails> deviceDetailsSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceDetails>();
        public void DeviceDetails(int DeviceID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devicedetails);
            HttpPayload devices = this.UrlBuilder.DeviceDetailsPayload(DeviceID);
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devicedetails(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devicedetails);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = deviceDetailsSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceDetails != null) OnDeviceDetails(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceCommands> deviceCommandsSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceCommands>();
        public void DeviceCommands(int DeviceID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommands);
            HttpPayload devices = this.UrlBuilder.DeviceCommandsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devicecommands(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommands);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = deviceCommandsSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceCommands != null) OnDeviceCommands(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceCommandResponse> deviceCommandsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceCommandResponse>();
        public void DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommand);
            HttpPayload devices = this.UrlBuilder.DeviceCommandPayload(DeviceID, Name, arg, type);
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devicecommand(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommand);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = deviceCommandsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceCommand != null) OnDeviceCommand(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceValues> deviceValuesResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceValues>();
        public void DeviceValues(int DeviceID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded);
            HttpPayload devices = this.UrlBuilder.DeviceValuesPayload(DeviceID);
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = deviceValuesResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceValues != null) OnDeviceValues(result);
        }

        Helpers.Serialization.ISerialize<Models.SceneResponse> SceneResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.SceneResponse>();
        public void Scenes()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_scenes);
            HttpPayload devices = this.UrlBuilder.ScenesPayload();
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_scenes(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_scenes);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = SceneResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnScenes != null) OnScenes(result);
        }


        Helpers.Serialization.ISerialize<Models.SceneNameChangeResponse> SceneNameChangeResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.SceneNameChangeResponse>();
        public void ChangeSceneName(int SceneID, string Name)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_changename);
            HttpPayload devices = this.UrlBuilder.ScenesChangeNamePayload(SceneID, Name);
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_changename(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_changename);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = SceneNameChangeResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnChangeSceneName != null) OnChangeSceneName(result);
        }

        public void StartScene(int SceneID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_startscene);
            HttpPayload devices = this.UrlBuilder.StartScenePayload(SceneID);
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);


        }

        void HttpClient_OnHttpDownloaded_startscene(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_startscene);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = SceneNameChangeResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnStartScene != null) OnStartScene(result);
        }

        Helpers.Serialization.ISerialize<Models.GroupsResponse> GroupsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.GroupsResponse>();
        public void Groups()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_groups);
            HttpPayload devices = this.UrlBuilder.GroupsPayload();
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);

        }

        void HttpClient_OnHttpDownloaded_groups(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_groups);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = GroupsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnGroups != null) OnGroups(result);
        }


        Helpers.Serialization.ISerialize<Models.GroupDetailsResponse> GroupDetailsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.GroupDetailsResponse>();
        public void GroupDetails(int GroupID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_groupdetails);
            HttpPayload devices = this.UrlBuilder.GroupDetailsPayload(GroupID);
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

        }

        void HttpClient_OnHttpDownloaded_groupdetails(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_groupdetails);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = GroupDetailsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnGroupDetails != null) OnGroupDetails(result);
        }

        Helpers.Serialization.ISerialize<Models.CommandsResponse> CommandsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.CommandsResponse>();
        public void Commands()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_commands);
            HttpPayload devices = this.UrlBuilder.CommandsPayload();
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

        }

        void HttpClient_OnHttpDownloaded_commands(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_commands);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = CommandsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnCommands != null) OnCommands(result);
        }

        public void SendCommand(Models.BuiltinCommand Command)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_sendcommand);
            HttpPayload devices = this.UrlBuilder.SendCommandsPayload(Command);
            devices.Cookies = this.Cookies;
            if (this.Headers.AllKeys.Contains(tokenKey)) devices.Headers[tokenKey] = this.Headers[tokenKey];
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_sendcommand(object Sender, byte[] Data, long Duration, string Key, WebHeaderCollection headers)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_sendcommand);
            if (headers.AllKeys.Contains(tokenKey)) this.Headers[tokenKey] = headers[tokenKey];
            var result = CommandsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnSendCommand != null) OnSendCommand(result);
        }
        
    } 
}
