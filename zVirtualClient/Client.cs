using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.Interfaces;

namespace zVirtualClient
{
    public class Client : IServiceController
    {
        IHttpClient HttpClient { get; set; }
        IServiceController VirtualScenesController { get; set; }
        public Credentials Credentials { get; set; }
        public Helpers.ILog Logger { get; set; }

        public Client(Credentials Credentials, IHttpClient HttpClient = null, IServiceController Controller = null)
        {
            if (HttpClient == null)
            {
                HttpClient = new HTTP.DesktopHttpClient(Credentials);
            }
            if (Controller == null)
            {
                Controller = new VirtualScenes34.VitualScenes34Controller(Credentials);
            }
            Helpers.LogManager.ConfigureLogging();
            Logger = new Helpers.log4netLogger<Client>();            
            this.VirtualScenesController = Controller;
            if(this.VirtualScenesController.Credentials == null) this.VirtualScenesController.Credentials = Credentials;
            
            Logger.Debug("New Client Created");
            Logger.DebugFormat("HttpClient:{0}", HttpClient.ToString());
            Logger.DebugFormat("Controller:{0}", Controller.ToString());
            Logger.DebugFormat("Credentials:{0}", Helpers.Serialization.JSONSerializer<Credentials>.ToJSON(Credentials));

        }


        public Models.LoginResponse Login()
        {
            return VirtualScenesController.Login();
        }
        public Models.LoginResponse Logout()
        {
            return VirtualScenesController.Logout();
        }
        public Models.Devices Devices()
        {
            return VirtualScenesController.Devices();
        }
        public Models.DeviceDetails DeviceDetails(int DeviceID)
        {
            return VirtualScenesController.DeviceDetails(DeviceID);
        }
        public Models.DeviceCommands DeviceCommands(int DeviceID)
        {
            return VirtualScenesController.DeviceCommands(DeviceID);
        }
        public Models.DeviceCommandResponse DeviceCommand(int DeviceID, string Name, int arg, string type)
        {
            return VirtualScenesController.DeviceCommand(DeviceID, Name, arg, type);
        }
        public Models.DeviceValues DeviceValues(int DeviceID)
        {
            return VirtualScenesController.DeviceValues(DeviceID);
        }

        public Models.SceneResponse Scenes()
        {
            return VirtualScenesController.Scenes();
        }

        public Models.SceneNameChangeResponse ChangeSceneName(int SceneID, string Name)
        {
            return VirtualScenesController.ChangeSceneName(SceneID, Name);
        }

        public Models.SceneNameChangeResponse StartScene(int SceneID)
        {
            return VirtualScenesController.StartScene(SceneID);
        }

        public Models.GroupsResponse Groups()
        {
            return VirtualScenesController.Groups();
        }
        public Models.GroupDetailsResponse GroupDetails(int GroupID)
        {
            return VirtualScenesController.GroupDetails(GroupID);
        }

        public Models.CommandsResponse Commands()
        {
            return VirtualScenesController.Commands();
        }
        public Models.CommandsResponse SendCommand(Models.BuiltinCommand Command)
        {
            return VirtualScenesController.SendCommand(Command);
        }
    }
}
