using CoreServiceLib.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using UnitTest.Models;
using UnitTest.Properties;

namespace UnitTest.API
{
    [TestFixture]
    internal class UserApiTest
    {
        [SetUp]
        public void SetUp()
        {
            _handler = new RequestHandler(Settings.Default.ApiAddress);
        }

        private RequestHandler _handler;

        #region test login
        [Test]
        public void TestLoginTrue()
        {
            var user = new User { email = "admin1@gmail.com", password = "12345" };
            string content = _handler.SendRequest(user, Settings.Default.LoginApiUrl, Method.POST);

            Assert.AreEqual(true, content.Contains("token"));
        }

        [Test]
        public void TestLoginFalse()
        {
            var user = new User { email = "admin1@gmail.com", password = "1234" };
            string content = _handler.SendRequest(user, Settings.Default.LoginApiUrl, Method.POST);

            Assert.AreEqual(false, content.Contains("token"));
        }

        #endregion

        #region test edit profile

        [Test]
        public void TestEditProfileTrueToken()
        {
            User user = new User()
            {
                token = "L/hiNJTU1EgbX2DRNUJeQoruqxXFBuvn",
                firstname = "Thiện",
                lastname = "Trần",
                postal_code = 1,
                country_id = 1,
                district_id = 1,
                state_id = 1,
                avatar = "default"
            };

            string content = _handler.SendRequest(user, Settings.Default.EditProfileApiUrl, Method.POST);

            Assert.AreEqual(true, content.Contains("success"));
        }

        [Test]
        public void TestEditProfileFalseToken()
        {
            User user = new User()
            {
                token = "L/hiNJTU1EgbX2DRNUJeQoruqxXFBuv",
                firstname = "Thiện",
                lastname = "Trần",
                postal_code = 1,
                country_id = 1,
                district_id = 1,
                state_id = 1,
                avatar = "default"
            };

            string content = _handler.SendRequest(user, Settings.Default.EditProfileApiUrl, Method.POST);

            Assert.AreEqual(true, content.Contains("error"));
        }

        #endregion
    }
}