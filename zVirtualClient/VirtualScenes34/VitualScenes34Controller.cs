using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.Interfaces;


namespace zVirtualClient.VirtualScenes34
{
    public class VitualScenes34Controller : IServiceController
    {
        Helpers.ILog log;
        System.Net.CookieContainer Cookies = new System.Net.CookieContainer();
        IUrlBuilder UrlBuilder;
        public Credentials Credentials { get; set; }
        IHttpClient HttpClient;
        

        public VitualScenes34Controller(Credentials Credentials, IHttpClient HttpClient = null)
        {
            log = new Helpers.log4netLogger<VitualScenes34Controller>();

            this.Credentials = Credentials;
            this.UrlBuilder = new VirtualScenes34UrlBuilder(this.Credentials);
            if (HttpClient == null)
                HttpClient = new Desktop.HttpClient(this.Credentials);

            this.HttpClient = HttpClient;
        }

        Helpers.Serialization.ISerialize<Models.LoginResponse> loginSerializer = new Helpers.Serialization.JSONSerializer<Models.LoginResponse>();
            
        public Models.LoginResponse Login()
        {
            HttpPayload login = this.UrlBuilder.LoginPayload();          

            login.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(login);            
            return loginSerializer.Deserialize(result);
        }

        public Models.LoginResponse Logout()
        {
            HttpPayload logout = this.UrlBuilder.LogoutPayload();
            logout.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(logout);
            return loginSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.Devices> devicesSerializer = new Helpers.Serialization.JSONSerializer<Models.Devices>();
        public Models.Devices Devices()
        {
            HttpPayload devices = this.UrlBuilder.DevicesPayload();
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);

            return devicesSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceDetails> deviceDetailsSerializer = new Helpers.Serialization.JSONSerializer<Models.DeviceDetails>();
        public Models.DeviceDetails DeviceDetails(int DeviceID)
        {
            HttpPayload devices = this.UrlBuilder.DeviceDetailsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            return deviceDetailsSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.DeviceCommands> deviceCommandsSerializer = new Helpers.Serialization.JSONSerializer<Models.DeviceCommands>();
        public Models.DeviceCommands DeviceCommands(int DeviceID)
        {
            HttpPayload devices = this.UrlBuilder.DeviceCommandsPayload(DeviceID);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            return deviceCommandsSerializer.Deserialize(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceCommandResponse> deviceCommandsResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.DeviceCommandResponse>();
        public Models.DeviceCommandResponse DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            HttpPayload devices = this.UrlBuilder.DeviceCommandPayload(DeviceID, Name, arg, type);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            return deviceCommandsResponseSerializer.Deserialize(result);
        }


        Helpers.Serialization.ISerialize<Models.DeviceValues> deviceValuesResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.DeviceValues>();
        public Models.DeviceValues DeviceValues(int DeviceID)
        {
            HttpPayload devices = this.UrlBuilder.DeviceValuesPayload(DeviceID);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            return deviceValuesResponseSerializer.Deserialize(result);
        }

        Helpers.Serialization.ISerialize<Models.SceneResponse> SceneResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.SceneResponse>();
        public Models.SceneResponse Scenes()
        {
            HttpPayload devices = this.UrlBuilder.ScenesPayload();
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            return SceneResponseSerializer.Deserialize(result);
        }


        Helpers.Serialization.ISerialize<Models.SceneNameChangeResponse> SceneNameChangeResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.SceneNameChangeResponse>();
        public Models.SceneNameChangeResponse ChangeSceneName(int SceneID, string Name)
        {
            HttpPayload devices = this.UrlBuilder.ScenesChangeNamePayload(SceneID, Name);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            return SceneNameChangeResponseSerializer.Deserialize(result);
        }

        public Models.SceneNameChangeResponse StartScene(int SceneID)
        {
            HttpPayload devices = this.UrlBuilder.StartScenePayload(SceneID);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);

            return SceneNameChangeResponseSerializer.Deserialize(result);

        }

        Helpers.Serialization.ISerialize<Models.GroupsResponse> GroupsResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.GroupsResponse>();
        public Models.GroupsResponse Groups()
        {
            HttpPayload devices = this.UrlBuilder.GroupsPayload();
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            //return result;
            return GroupsResponseSerializer.Deserialize(result);

        }


        Helpers.Serialization.ISerialize<Models.GroupDetailsResponse> GroupDetailsResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.GroupDetailsResponse>();
        public Models.GroupDetailsResponse GroupDetails(int GroupID)
        {
            HttpPayload devices = this.UrlBuilder.GroupDetailsPayload(GroupID);
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            //return result;
            return GroupDetailsResponseSerializer.Deserialize(result);

        }

        Helpers.Serialization.ISerialize<Models.CommandsResponse> CommandsResponseSerializer = new Helpers.Serialization.JSONSerializer<Models.CommandsResponse>();
        public Models.CommandsResponse Commands()
        {
            HttpPayload devices = this.UrlBuilder.CommandsPayload();
            devices.Cookies = this.Cookies;
            string result = HttpClient.HTTPAsString(devices);
            //return result;
            return CommandsResponseSerializer.Deserialize(result);

        }

        
    } 
}
