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
            Models.LoginResult result = client.Login();
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

    }
}