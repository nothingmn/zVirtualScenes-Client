//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;

//namespace zVirtualClient.Tests
//{
//    [TestFixture]
//    public class AuthenticationTests
//    {
  
//        [Test]
//        public void Login()
//        {

//            //arrange
//            Client client = new Client(Mother.Credentials);
//            //act
//            Models.LoginResponse result = client.Login();
//            //assert
//            Assert.IsTrue(result.success);
//        }

//        [Test]
//        public void Logout()
//        {

//            //arrange
//            Client client = new Client(Mother.Credentials);
//            Models.LoginResponse result = client.Login();
//            if (result.success)
//            {
//                //act
//                result = client.Logout();
//                //assert
//                Assert.IsTrue((result.success && result.isLoggedIn == false));

//            }
//            else
//            {
//                Assert.IsTrue(false, "Login failed");
//            }
//        }
//    }
//}