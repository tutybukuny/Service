using CoreServiceLib.DAO;
using CoreServiceLib.Models;
using NUnit.Framework;
using UnitTest.Properties;

namespace UnitTest.DAO
{
    [TestFixture]
    internal class TokenDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            dao = new TokenDao(Settings.Default.ConnectionString);
        }

        private TokenDao dao;

        [Test]
        public void TestInsert()
        {
            var tokenString = dao.AutoGenerate();
            var token = new Token {TokenString = tokenString, User = new User {id = 1}};
            Assert.AreEqual(true, dao.Insert(token));
        }

        [Test]
        public void TestCheckToken()
        {
            string token = "j1+1scnT1Eij2AqdUYdoRKYby6Hjvxu6";
            Assert.AreEqual(true, dao.CheckToken(token));
        }
    }
}