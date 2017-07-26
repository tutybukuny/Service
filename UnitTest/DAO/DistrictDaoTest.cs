using System;
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
    internal class DistrictDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            dao = new DistrictDao(Settings.Default.ConnectionString);
        }

        private DistrictDao dao;

        [Test]
        public void TestDelete()
        {
            var district = new District { Id = 5 };
            Assert.AreEqual(true, dao.Delete(district));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var district = new District { Id = 101212 };
            Assert.AreEqual(true, dao.Delete(district));
        }

        [Test]
        public void TestGetAll()
        {
            var districts = dao.GetAll();
            var stateDao = new StateDao(Settings.Default.ConnectionString);
            var state = stateDao.GetById(1);
            var expecteDistricts = new List<District>
            {
                new District {Id = 1, Name = "Hà Đông", State = state},
                new District {Id = 2, Name = "Cầu Giấy", State = state},
                new District {Id = 4, Name = "Đống Đa", State = state}
            };

            var result = true;

            for (var i = 0; i < expecteDistricts.Count; i++)
                result = expecteDistricts[i].MyEquals(districts[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var stateDao = new StateDao(Settings.Default.ConnectionString);
            var state = stateDao.GetById(1);
            var expectedDistrict = new District { Id = 1, Name = "Hà Đông", State = state };
            var district = dao.GetById(1);

            Assert.AreEqual(true, district.MyEquals(expectedDistrict));
        }

        [Test]
        public void TestGetByNotId()
        {
            var district = dao.GetById(11455);

            Assert.AreEqual(null, district);
        }

        [Test]
        public void TestInsert()
        {
            var district = new District { Name = "Hoàng Cầu", State = new State { Id = 1 } };
            Assert.AreEqual(true, dao.Insert(district));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var district = new District { Name = "Hoàng Cầu", State = new State { Id = 1 } };
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullName()
        {
            var district = new District { Name = null, State = new State() { Id = 1 } };
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullState()
        {
            var district = new District { Id = 6, Name = "Một cái tên xa lạ", State = null };
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdate()
        {
            var district = new District { Id = 6, Name = "Hai Bà Trưng", State = new State { Id = 1 } };
            Assert.AreEqual(true, dao.Update(district));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var district = new District { Id = 1324, Name = "Hai Bà Trưng", State = new State { Id = 1 } };
            Assert.AreEqual(true, dao.Update(district));
        }

        [Test]
        public void TestUpdateNullState()
        {
            var district = new District { Id = 6, Name = "Hai Bà Trưng", State = null };
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdateNullName()
        {
            var district = new District { Id = 11, Name = null, State = new State { Id = 1 } };
            ActualValueDelegate<object> e = () => dao.Update(district);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
    }
}