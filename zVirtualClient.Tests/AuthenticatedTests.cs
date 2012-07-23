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
        Client client = new Client(Mother.Credentials);
     
        [SetUp]
        public void Login()
        {            
            Models.LoginResponse result = client.Login();
            if (!result.success) throw new System.Security.SecurityException("Could not login");
        }

        [TearDown]
        public void Logout()
        {
            client.Logout();
        }

        [Test]
        public void DevicesList()
        {
            //act
            Models.Devices devices = client.Devices();
            //assert
            Assert.IsTrue(devices != null, "Count:" + devices.devices.Count);
            Assert.IsTrue(devices.success);

        }

        [Test]
        public void Device11Details()
        {
            var a = client.DeviceDetails(11);
            Assert.IsNotNull(a);
            Assert.IsTrue(a.success);
            Assert.IsNotNull(a.details.id);
        }


        [Test]
        public void Device11Commands()
        {
            var a = client.DeviceCommands(11);
            Assert.IsNotNull(a);
            Assert.IsTrue(a.success);
            Assert.IsNotNull(a.device_commands);
        }


        [Test]
        public void Device11OffCommands()
        {
            var a = client.DeviceCommand(11, "DYNAMIC_CMD_BASIC", 0, "device");
            Assert.IsNotNull(a);
            Assert.IsTrue(a.success);

        }
        [Test]
        public void Device11OnCommands()
        {
            var a = client.DeviceCommand(11, "DYNAMIC_CMD_BASIC", 255, "device");
            Assert.IsNotNull(a);
            Assert.IsTrue(a.success);
        }
        [Test]
        public void Device11Values()
        {
            var a = client.DeviceValues(11);
            Assert.IsNotNull(a);
            Assert.IsTrue(a.success);
        }

        [Test]
        public void Scenes()
        {
            var a = client.Scenes();
            Assert.IsNotNull(a);
            Assert.IsTrue(a.success);
        }

        [Test]
        public void ChangeSceneID1sName()
        {
            int sceneID = 1;
            string newName = "Movie Mode";
            var result = client.ChangeSceneName(sceneID, newName);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
            Assert.IsTrue(result.desc == "Scene Name Updated.");
        }


        [Test]
        public void StartScene1()
        {
            int sceneID = 1;
            var result = client.StartScene(sceneID);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
            Assert.IsTrue(result.desc == "Scene Started.");
        }


        [Test]
        public void Groups()
        {
            int sceneID = 1;
            var result = client.Groups();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
        }


        [Test]
        public void Group1Details()
        {
            int groupID = 1;
            var result = client.GroupDetails(groupID);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
        }


        [Test]
        public void Commands()
        {
            var result = client.Commands();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
        }


        //[Test]
        //public void TurnGroup1OffByCommand()
        //{
        //    var result = client.SendCommand(new Models.BuiltinCommand() { arg = 1, friendlyname = "Turn Group Off", name = "GROUP_OFF", helptext = "", id = 3 });
        //    Assert.IsNotNull(result);
        //    //Assert.IsTrue(result.success);
        //}
        [Test]
        public void CommandByIDAndArgGroup1On()
        {
            var result = client.SendCommand(new Models.BuiltinCommand() { arg = 1, id = 3 });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
        }
        [Test]
        public void CommandByIDAndArgGroup1Off()
        {
            var result = client.SendCommand(new Models.BuiltinCommand() { arg = 1, id = 4 });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.success);
        }


    }
}


