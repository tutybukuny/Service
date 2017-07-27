using CoreServiceLib.Models;
using NUnit.Framework;
using RestSharp;
using UnitTest.Models;
using UnitTest.Properties;

namespace UnitTest.API
{
    [TestFixture]
    internal class LoginApiTest
    {
        [SetUp]
        public void SetUp()
        {
            handler = new RequestHandler(Settings.Default.ApiAddress);
        }

        private RequestHandler handler;

        [Test]
        public void TestLoginTrue()
        {
            var user = new User {email = "admin1@gmail.com", password = "12345"};
            string content = handler.SendRequest(user, Settings.Default.LoginApiUrl, Method.POST);

            Assert.AreEqual(true, content.Contains("token"));
        }

        [Test]
        public void TestLoginFalse()
        {
            var user = new User { email = "admin1@gmail.com", password = "1234" };
            string content = handler.SendRequest(user, Settings.Default.LoginApiUrl, Method.POST);

            Assert.AreEqual(false, content.Contains("token"));
        }
    }
}