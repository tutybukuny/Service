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

        #region delete testing
        [Test]
        public void TestDelete()
        {
            var state = new State { id = 7 };
            Assert.AreEqual(true, dao.Delete(state));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var state = new State { id = 101212 };
            Assert.AreEqual(true, dao.Delete(state));
        }
        #endregion

        #region get item testing
        [Test]
        public void TestGetAll()
        {
            var states = dao.GetAll();
            var expectedStates = new List<State>
            {
                new State {id = 1, name = "Hà Nội", country_id = 1},
                new State {id = 2, name = "Thành Phố Hồ Chí Minh", country_id = 1},
                new State {id = 3, name = "Hà Tây", country_id = 1}
            };

            var result = true;

            for (var i = 0; i < expectedStates.Count; i++)
                result = expectedStates[i].MyEquals(states[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var expectedState = new State { id = 1, name = "Hà Nội", country_id = 1 };
            var state = dao.GetById(1);

            Assert.AreEqual(true, expectedState.MyEquals(state));
        }

        [Test]
        public void TestGetByNotId()
        {
            var state = dao.GetById(11455);

            Assert.AreEqual(null, state);
        }
        #endregion

        #region insert testing
        [Test]
        public void TestInsert()
        {
            var state = new State { name = "Quảng Bình", country_id = 1 };
            Assert.AreEqual(true, dao.Insert(state));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var state = new State { name = "Quảng Bình", country_id = 1 };
            ActualValueDelegate<object> e = () => dao.Insert(state);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullCountry()
        {
            var state = new State { name = "Một cái tên xa lạ" };
            ActualValueDelegate<object> e = () => dao.Insert(state);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestInsertNullName()
        {
            var state = new State { name = null, country_id = 1 };
            ActualValueDelegate<object> e = () => dao.Insert(state);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
        #endregion

        #region update testing
        [Test]
        public void TestUpdate()
        {
            var state = new State { id = 7, name = "Quảng Ngãi", country_id = 1 };
            Assert.AreEqual(true, dao.Update(state));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var state = new State { id = 1234, name = "Quảng Ngãi", country_id = 1 };
            Assert.AreEqual(true, dao.Update(state));
        }

        [Test]
        public void TestUpdateNullCountry()
        {
            var state = new State { id = 7, name = null };
            ActualValueDelegate<object> e = () => dao.Update(state);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdateNullName()
        {
            var state = new State { id = 7, name = null, country_id = 1 };
            ActualValueDelegate<object> e = () => dao.Update(state);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
        #endregion
    }
}