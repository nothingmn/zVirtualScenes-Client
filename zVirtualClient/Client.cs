﻿using System;
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
                HttpClient = new Desktop.HttpClient(Credentials);
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


        public Models.LoginResult Login()
        {
            return VirtualScenesController.Login();
        }
        public Models.LoginResult Logout()
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
    }
}
