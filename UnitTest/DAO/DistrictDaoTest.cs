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
            var district = new District {id = 5};
            Assert.AreEqual(true, dao.Delete(district));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var district = new District {id = 101212};
            Assert.AreEqual(true, dao.Delete(district));
        }

        [Test]
        public void TestGetAll()
        {
            var districts = dao.GetAll();
            var expecteDistricts = new List<District>
            {
                new District {id = 1, name = "Hà Đông", state_id = 1},
                new District {id = 2, name = "Cầu Giấy", state_id = 1},
                new District {id = 4, name = "Đống Đa", state_id = 1}
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
            var expectedDistrict = new District {id = 1, name = "Hà Đông", state_id = 1};
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
            var district = new District {name = "Hoàng Cầu", state_id = 1};
            Assert.AreEqual(true, dao.Insert(district));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var district = new District {name = "Hoàng Cầu", state_id = 1};
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullName()
        {
            var district = new District {name = null, state_id = 1};
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullState()
        {
            var district = new District {id = 6, name = "Một cái tên xa lạ"};
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdate()
        {
            var district = new District {id = 6, name = "Hai Bà Trưng", state_id = 1};
            Assert.AreEqual(true, dao.Update(district));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var district = new District {id = 1324, name = "Hai Bà Trưng", state_id = 1};
            Assert.AreEqual(true, dao.Update(district));
        }

        [Test]
        public void TestUpdateNullName()
        {
            var district = new District {id = 11, name = null, state_id = 1};
            ActualValueDelegate<object> e = () => dao.Update(district);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestUpdateNullState()
        {
            var district = new District {id = 6, name = "Hai Bà Trưng"};
            ActualValueDelegate<object> e = () => dao.Insert(district);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }
    }
}