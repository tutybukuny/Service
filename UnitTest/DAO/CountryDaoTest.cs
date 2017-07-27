using System.Collections.Generic;
using System.Data.SqlClient;
using CoreServiceLib.DAO;
using CoreServiceLib.Models;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnitTest.Properties;

namespace UnitTest.DAO
{
    [TestFixture]
    public class CountryDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            dao = new CountryDao(Settings.Default.ConnectionString);
        }

        private CountryDao dao;

        #region delete testing
        [Test]
        public void TestDelete()
        {
            var country = new Country { id = 11, name = "Venezuela" };
            Assert.AreEqual(true, dao.Delete(country));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var country = new Country { id = 101212, name = "Venezuela" };
            Assert.AreEqual(true, dao.Delete(country));
        }
        #endregion

        #region get item testing
        [Test]
        public void TestGetAll()
        {
            var countries = dao.GetAll();
            var expectedCountries = new List<Country>
            {
                new Country {id = 1, name = "Việt Nam"},
                new Country {id = 2, name = "Lào"},
                new Country {id = 3, name = "Campuchia"}
            };

            var result = true;

            for (var i = 0; i < expectedCountries.Count; i++)
                result = expectedCountries[i].MyEquals(countries[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var expectedCountry = new Country { id = 1, name = "Việt Nam" };
            var country = dao.GetById(1);

            Assert.AreEqual(true, country.MyEquals(expectedCountry));
        }

        [Test]
        public void TestGetByNotId()
        {
            var country = dao.GetById(11455);

            Assert.AreEqual(null, country);
        }
        #endregion

        #region insert testing
        [Test]
        public void TestInsert()
        {
            var country = new Country { name = "Cai Lao" };
            Assert.AreEqual(true, dao.Insert(country));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var country = new Country { name = "Việt Nam" };
            ActualValueDelegate<object> e = () => dao.Insert(country);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullName()
        {
            var country = new Country { name = null };
            ActualValueDelegate<object> e = () => dao.Insert(country);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
        #endregion

        #region update testing
        [Test]
        public void TestUpdate()
        {
            var country = new Country { id = 11, name = "Venezuela" };
            Assert.AreEqual(true, dao.Update(country));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var country = new Country { id = 1324, name = "Venezuela" };
            Assert.AreEqual(true, dao.Update(country));
        }

        [Test]
        public void TestUpdateNullName()
        {
            var country = new Country { id = 11, name = null };
            ActualValueDelegate<object> e = () => dao.Update(country);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
        #endregion
    }
}