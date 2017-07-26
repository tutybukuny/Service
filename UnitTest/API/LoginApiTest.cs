using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreServiceLib.Models;
using NUnit.Framework;
using RestSharp;
using UnitTest.Models;
using UnitTest.Properties;

namespace UnitTest.API
{
    [TestFixture]
    class LoginApiTest
    {
        private RequestHandler handler;

        [SetUp]
        public void SetUp()
        {
            handler = new RequestHandler(Settings.Default.ApiAddress);
        }

        [Test]
        public void TestLoginTrue()
        {
            User user = new User(){email = "admin1@gmail.com", password = "12345"};
            var content = handler.SendRequest(user, Settings.Default.LoginApiUrl, Method.POST);
        }
    }
}
