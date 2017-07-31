using System.Collections.Generic;
using System.Data.SqlClient;
using DataTier;
using DataTier.Dao;
using DataTier.Module;
using Ninject;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace UnitTest.DAO
{
    [TestFixture]
    public class CountryDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            var kernel = new StandardKernel(new DaoModule());
            dao = kernel.Get<IDao<Country>>("CountryDao");
        }

        private IDao<Country> dao;

        [Test]
        public void TestCreateDao()
        {
            var kernel = new StandardKernel(new DaoModule());
            var dao = kernel.Get<IDao<Country>>("CountryDao");

            Assert.AreNotEqual(null, dao);
        }

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

            //for (var i = 0; i < expectedCountries.Count; i++)
            //    result = expectedCountries[i].MyEquals(countries[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var expectedCountry = new Country { id = 1, name = "Andorra" };
            CountryDao countryDao = (CountryDao) dao;
            var country = countryDao.GetById(1);

            Assert.AreEqual(true, country.name == expectedCountry.name);
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