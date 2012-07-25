using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace zVirtualClient.Tests
{
    [TestFixture]
    public class AuthenticatedTests
    {

        [Test]
        public void Login()
        {
            Client client = new Client(Mother.Credentials);
            client.OnLogin += new Interfaces.LoginResponse(client_OnLogin);
            client.OnError += new Interfaces.Error(client_OnError);
            client.Login();
            while (!loginComplete)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        void client_OnError(object Sender, string Message, Exception Exception)
        {
            Assert.Fail(Message, Sender, Exception);
            loginComplete = true;
        }

        bool loginComplete = false;
        void client_OnLogin(Models.LoginResponse LoginResponse)
        {           
            loginComplete = true;
            Assert.IsTrue(LoginResponse.success);
        }

        //[Test]
        //public void DevicesList()
        //{
        //    //act
        //    Models.Devices devices = client.Devices();
        //    //assert
        //    Assert.IsTrue(devices != null, "Count:" + devices.devices.Count);
        //    Assert.IsTrue(devices.success);

        //}

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


