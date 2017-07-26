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
    internal class StateDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            dao = new StateDao(Settings.Default.ConnectionString);
        }

        private StateDao dao;

        [Test]
        public void TestDelete()
        {
            var state = new State { Id = 7 };
            Assert.AreEqual(true, dao.Delete(state));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var state = new State { Id = 101212 };
            Assert.AreEqual(true, dao.Delete(state));
        }

        [Test]
        public void TestGetAll()
        {
            var states = dao.GetAll();
            var expectedStates = new List<State>
            {
                new State {Id = 1, Name = "Hà Nội", Country = new Country {Id = 1, Name = "Việt Nam"}},
                new State {Id = 2, Name = "Thành Phố Hồ Chí Minh", Country = new Country {Id = 1, Name = "Việt Nam"}},
                new State {Id = 3, Name = "Hà Tây", Country = new Country {Id = 1, Name = "Việt Nam"}}
            };

            var result = true;

            for (var i = 0; i < expectedStates.Count; i++)
                result = expectedStates[i].MyEquals(states[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var expectedState = new State { Id = 1, Name = "Hà Nội", Country = new Country { Id = 1, Name = "Việt Nam" } };
            var state = dao.GetById(1);

            Assert.AreEqual(true, expectedState.MyEquals(state));
        }

        [Test]
        public void TestGetByNotId()
        {
            var state = dao.GetById(11455);

            Assert.AreEqual(null, state);
        }

        [Test]
        public void TestInsert()
        {
            var state = new State { Name = "Quảng Bình", Country = new Country { Id = 1 } };
            Assert.AreEqual(true, dao.Insert(state));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var state = new State { Name = "Quảng Bình", Country = new Country { Id = 1 } };
            ActualValueDelegate<object> e = () => dao.Insert(state);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullCountry()
        {
            var state = new State { Name = "Một cái tên xa lạ", Country = null };
            ActualValueDelegate<object> e = () => dao.Insert(state);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestInsertNullName()
        {
            var state = new State { Name = null, Country = new Country { Id = 1 } };
            ActualValueDelegate<object> e = () => dao.Insert(state);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestUpdate()
        {
            var state = new State { Id = 7, Name = "Quảng Ngãi", Country = new Country { Id = 1 } };
            Assert.AreEqual(true, dao.Update(state));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var state = new State { Id = 1234, Name = "Quảng Ngãi", Country = new Country { Id = 1 } };
            Assert.AreEqual(true, dao.Update(state));
        }

        [Test]
        public void TestUpdateNullCountry()
        {
            var state = new State { Id = 7, Name = null, Country = null };
            ActualValueDelegate<object> e = () => dao.Update(state);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdateNullName()
        {
            var state = new State { Id = 7, Name = null, Country = new Country { Id = 1 } };
            ActualValueDelegate<object> e = () => dao.Update(state);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
    }
}