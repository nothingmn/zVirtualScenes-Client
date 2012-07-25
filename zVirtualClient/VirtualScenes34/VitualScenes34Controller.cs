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
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_logout);
            HttpPayload logout = this.UrlBuilder.LogoutPayload();
            logout.Cookies = this.Cookies;
            HttpClient.HTTPAsString(logout);
            
        }

        void HttpClient_OnHttpDownloaded_logout(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_logout);
            var result = loginSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnLogout != null) OnLogout(result);
        }

        Helpers.Serialization.ISerialize<Models.Devices> devicesSerializer = new Helpers.Serialization.NewtonSerializer<Models.Devices>();
        public void Devices()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devices);
            HttpPayload devices = this.UrlBuilder.DevicesPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devices(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_logout);
            var result = devicesSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDevices != null) OnDevices(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceDetails> deviceDetailsSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceDetails>();
        public void DeviceDetails(int DeviceID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devicedetails);
            HttpPayload devices = this.UrlBuilder.DeviceDetailsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devicedetails(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devicedetails);
            var result = deviceDetailsSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceDetails != null) OnDeviceDetails(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceCommands> deviceCommandsSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceCommands>();
        public void DeviceCommands(int DeviceID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommands);
            HttpPayload devices = this.UrlBuilder.DeviceCommandsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devicecommands(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommands);
            var result = deviceCommandsSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceCommands != null) OnDeviceCommands(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceCommandResponse> deviceCommandsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceCommandResponse>();
        public void DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommand);
            HttpPayload devices = this.UrlBuilder.DeviceCommandPayload(DeviceID, Name, arg, type);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_devicecommand(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_devicecommand);
            var result = deviceCommandsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceCommand != null) OnDeviceCommand(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceValues> deviceValuesResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.DeviceValues>();
        public void DeviceValues(int DeviceID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded);
            HttpPayload devices = this.UrlBuilder.DeviceValuesPayload(DeviceID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded);
            var result = deviceValuesResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnDeviceValues != null) OnDeviceValues(result);
        }

        Helpers.Serialization.ISerialize<Models.SceneResponse> SceneResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.SceneResponse>();
        public void Scenes()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_scenes);
            HttpPayload devices = this.UrlBuilder.ScenesPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_scenes(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_scenes);
            var result = SceneResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnScenes != null) OnScenes(result);
        }


        Helpers.Serialization.ISerialize<Models.SceneNameChangeResponse> SceneNameChangeResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.SceneNameChangeResponse>();
        public void ChangeSceneName(int SceneID, string Name)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_changename);
            HttpPayload devices = this.UrlBuilder.ScenesChangeNamePayload(SceneID, Name);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_changename(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_changename);
            var result = SceneNameChangeResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnChangeSceneName != null) OnChangeSceneName(result);
        }

        public void StartScene(int SceneID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_startscene);
            HttpPayload devices = this.UrlBuilder.StartScenePayload(SceneID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);


        }

        void HttpClient_OnHttpDownloaded_startscene(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_startscene);
            var result = SceneNameChangeResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnStartScene != null) OnStartScene(result);
        }

        Helpers.Serialization.ISerialize<Models.GroupsResponse> GroupsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.GroupsResponse>();
        public void Groups()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_groups);
            HttpPayload devices = this.UrlBuilder.GroupsPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

        }

        void HttpClient_OnHttpDownloaded_groups(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_groups);
            var result = GroupsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnGroups != null) OnGroups(result);
        }


        Helpers.Serialization.ISerialize<Models.GroupDetailsResponse> GroupDetailsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.GroupDetailsResponse>();
        public void GroupDetails(int GroupID)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_groupdetails);
            HttpPayload devices = this.UrlBuilder.GroupDetailsPayload(GroupID);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

        }

        void HttpClient_OnHttpDownloaded_groupdetails(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_groupdetails);
            var result = GroupDetailsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnGroupDetails != null) OnGroupDetails(result);
        }

        Helpers.Serialization.ISerialize<Models.CommandsResponse> CommandsResponseSerializer = new Helpers.Serialization.NewtonSerializer<Models.CommandsResponse>();
        public void Commands()
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_commands);
            HttpPayload devices = this.UrlBuilder.CommandsPayload();
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);

        }

        void HttpClient_OnHttpDownloaded_commands(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_commands);
            var result = CommandsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnCommands != null) OnCommands(result);
        }

        public void SendCommand(Models.BuiltinCommand Command)
        {
            HttpClient.OnHttpDownloaded += new HttpDownloaded(HttpClient_OnHttpDownloaded_sendcommand);
            HttpPayload devices = this.UrlBuilder.SendCommandsPayload(Command);
            devices.Cookies = this.Cookies;
            HttpClient.HTTPAsString(devices);
        }

        void HttpClient_OnHttpDownloaded_sendcommand(object Sender, byte[] Data, long Duration, string Key)
        {
            HttpClient.OnHttpDownloaded -= new HttpDownloaded(HttpClient_OnHttpDownloaded_sendcommand);
            var result = CommandsResponseSerializer.Deserialize(System.Text.Encoding.UTF8.GetString(Data, 0, Data.Length));
            if (OnSendCommand != null) OnSendCommand(result);
        }
        
    } 
}
