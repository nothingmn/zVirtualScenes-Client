using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace zVirtualClient.Tests
{
    [TestFixture]
    public class AuthenticatedTests
    {
        AutoResetEvent testTrigger;
        Client client = new Client(Mother.Credentials);
        [SetUp]
        public void setup()
        {
            client.OnLogin += new Interfaces.LoginResponse(client_OnLogin);
            client.OnError += new Interfaces.Error(client_OnError);
            client.OnChangeSceneName += new Interfaces.SceneNameChangeResponse(client_OnChangeSceneName);
            client.OnCommands += new Interfaces.CommandsResponse(client_OnCommands);
            client.OnDeviceCommand += new Interfaces.DeviceCommandResponse(client_OnDeviceCommand);
            client.OnDeviceCommands += new Interfaces.DeviceCommandsResponse(client_OnDeviceCommands);
            client.OnDeviceDetails += new Interfaces.DeviceDetailsResponse(client_OnDeviceDetails);
            client.OnDevices += new Interfaces.DevicesResponse(client_OnDevices);
            client.OnDeviceValues += new Interfaces.DeviceValuesResponse(client_OnDeviceValues);
            client.OnGroupDetails += new Interfaces.GroupDetailsResponse(client_OnGroupDetails);
            client.OnGroups += new Interfaces.GroupsResponse(client_OnGroups);
            client.OnLogout += new Interfaces.LogoutResponse(client_OnLogout);
            client.OnScenes += new Interfaces.SceneResponse(client_OnScenes);
            client.OnSendCommand += new Interfaces.CommandsResponse(client_OnSendCommand);
            client.OnStartScene += new Interfaces.SceneNameChangeResponse(client_OnStartScene);

            testTrigger = new AutoResetEvent(false);

        }

        void client_OnStartScene(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            response = SceneNameChangeResponse;
            testTrigger.Set();
        }

        void client_OnSendCommand(Models.CommandsResponse CommandsResponse)
        {
            response = CommandsResponse;
            testTrigger.Set();
        }

        void client_OnScenes(Models.SceneResponse SceneResponse)
        {
            response = SceneResponse;
            testTrigger.Set();
        }

        void client_OnLogout(Models.LoginResponse LoginResponse)
        {
            response = LoginResponse;
            testTrigger.Set();
        }

        void client_OnGroups(Models.GroupsResponse GroupsResponse)
        {
            response = GroupsResponse;
            testTrigger.Set();
        }

        void client_OnGroupDetails(Models.GroupDetailsResponse GroupDetailsResponse)
        {
            response = GroupDetailsResponse;
            testTrigger.Set();
        }

        void client_OnDeviceValues(Models.DeviceValues DeviceValuesResponse)
        {
            response = DeviceValuesResponse;
            testTrigger.Set();
        }

        void client_OnDevices(Models.Devices DevicesResponse)
        {
            response = DevicesResponse;
            testTrigger.Set();
        }

        void client_OnDeviceDetails(Models.DeviceDetails DeviceDetailsResponse)
        {
            response = DeviceDetailsResponse;
            testTrigger.Set();
        }

        void client_OnDeviceCommands(Models.DeviceCommands DeviceCommandsResponse)
        {
            response = DeviceCommandsResponse;
            testTrigger.Set();
        }

        void client_OnDeviceCommand(Models.DeviceCommandResponse DeviceCommandResponse)
        {
            response = DeviceCommandResponse;
            testTrigger.Set();
        }

        void client_OnCommands(Models.CommandsResponse CommandsResponse)
        {
            response = CommandsResponse;
            testTrigger.Set();
        }

        void client_OnChangeSceneName(Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            response = SceneNameChangeResponse;
            testTrigger.Set();
        }
        
        [Test]
        public void Login()
        {
            testTrigger = new AutoResetEvent(false);
            client.Login();
            testTrigger.WaitOne();
            Assert.IsTrue(((Models.LoginResponse)response).success);

            client.Devices();
            testTrigger.WaitOne();

            //assert
            Models.Devices DevicesResponse = (Models.Devices)response;
            Assert.IsTrue(DevicesResponse != null, "Count:" + DevicesResponse.devices.Count);
            Assert.IsTrue(DevicesResponse.success);

        }
        void client_OnError(object Sender, string Message, Exception Exception)
        {
            Assert.Fail(Message, Sender, Exception);
        }

        object response;
        void client_OnLogin(Models.LoginResponse LoginResponse)
        {
            response = LoginResponse;
            testTrigger.Set();
        }
        
        //[Test]
        //public void Device11Details()
        //{
        //    var a = client.DeviceDetails(11);
        //    Assert.IsNotNull(a);
        //    Assert.IsTrue(a.success);
        //    Assert.IsNotNull(a.details.id);
        //}


        //[Test]
        //public void Device11Commands()
        //{
        //    var a = client.DeviceCommands(11);
        //    Assert.IsNotNull(a);
        //    Assert.IsTrue(a.success);
        //    Assert.IsNotNull(a.device_commands);
        //}


        //[Test]
        //public void Device11OffCommands()
        //{
        //    var a = client.DeviceCommand(11, "DYNAMIC_CMD_BASIC", 0, "device");
        //    Assert.IsNotNull(a);
        //    Assert.IsTrue(a.success);

        //}
        //[Test]
        //public void Device11OnCommands()
        //{
        //    var a = client.DeviceCommand(11, "DYNAMIC_CMD_BASIC", 255, "device");
        //    Assert.IsNotNull(a);
        //    Assert.IsTrue(a.success);
        //}
        //[Test]
        //public void Device11Values()
        //{
        //    var a = client.DeviceValues(11);
        //    Assert.IsNotNull(a);
        //    Assert.IsTrue(a.success);
        //}

        //[Test]
        //public void Scenes()
        //{
        //    var a = client.Scenes();
        //    Assert.IsNotNull(a);
        //    Assert.IsTrue(a.success);
        //}

        //[Test]
        //public void ChangeSceneID1sName()
        //{
        //    int sceneID = 1;
        //    string newName = "Movie Mode";
        //    var result = client.ChangeSceneName(sceneID, newName);
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //    Assert.IsTrue(result.desc == "Scene Name Updated.");
        //}


        //[Test]
        //public void StartScene1()
        //{
        //    int sceneID = 1;
        //    var result = client.StartScene(sceneID);
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //    Assert.IsTrue(result.desc == "Scene Started.");
        //}


        //[Test]
        //public void Groups()
        //{
        //    int sceneID = 1;
        //    var result = client.Groups();
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //}


        //[Test]
        //public void Group1Details()
        //{
        //    int groupID = 1;
        //    var result = client.GroupDetails(groupID);
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //}


        //[Test]
        //public void Commands()
        //{
        //    var result = client.Commands();
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //}


        ////[Test]
        ////public void TurnGroup1OffByCommand()
        ////{
        ////    var result = client.SendCommand(new Models.BuiltinCommand() { arg = 1, friendlyname = "Turn Group Off", name = "GROUP_OFF", helptext = "", id = 3 });
        ////    Assert.IsNotNull(result);
        ////    //Assert.IsTrue(result.success);
        ////}
        //[Test]
        //public void CommandByIDAndArgGroup1On()
        //{
        //    var result = client.SendCommand(new Models.BuiltinCommand() { arg = 1, id = 3 });
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //}
        //[Test]
        //public void CommandByIDAndArgGroup1Off()
        //{
        //    var result = client.SendCommand(new Models.BuiltinCommand() { arg = 1, id = 4 });
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.success);
        //}


    }
}


