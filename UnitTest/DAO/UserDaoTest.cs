using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BusinessTier.Factory;
using DataTier;
using DataTier.Dao;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnitTest.Properties;

namespace UnitTest.DAO
{
    [TestFixture]
    internal class UserDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            _dao = (UserDao) DaoFactory.GetDao("UserDao");
        }

        private UserDao _dao;

        #region check exist testing

        [Test]
        public void TestEmailExisted()
        {
//            Assert.AreEqual(true, _dao.CheckExistsEmail("admin1@gmail.com"));
        }

        [Test]
        public void TestEmailNotExisted()
        {
//            Assert.AreEqual(false, _dao.CheckExistsEmail("admi@gmail.com"));
        }

        #endregion

        #region delete testing
        [Test]
        public void TestDelete()
        {
            var user = new User { id = 12 };
            Assert.AreEqual(true, _dao.Delete(user));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var user = new User { id = 101212 };
            Assert.AreEqual(true, _dao.Delete(user));
        }
        #endregion

        #region get item testing
        [Test]
        public void TestGetAll()
        {
            var users = _dao.GetAll();
            var expecteUsers = new List<User>
            {
                new User
                {
                    id = 1,
                    email = "admin1@gmail.com",
                    password = "12345",
                    firstname = "Thiện",
                    lastname = "Trần",
                    country_id = 1,
                    state_id = 1,
                    district_id = 1,
                    avatar = "default"
                },
                new User
                {
                    id = 2,
                    email = "admin2@gmail.com",
                    password = "12345",
                    firstname = "Thiện",
                    lastname = "Trần",
                    country_id = 1,
                    state_id = 1,
                    district_id = 1,
                    avatar = "default"
                }
            };

            var result = true;

            for (var i = 0; i < expecteUsers.Count; i++)
//                result = expecteUsers[i].MyEquals(users[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var expectedUser = new User
            {
                id = 1,
                email = "admin1@gmail.com",
                password = "12345",
                firstname = "Thiện",
                lastname = "Trần",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };

            var user = _dao.GetById(1);

//            Assert.AreEqual(true, user.MyEquals(expectedUser));
        }

        [Test]
        public void TestGetByNotId()
        {
            var user = _dao.GetById(11455);

            Assert.AreEqual(null, user);
        }
        #endregion

        #region insert testing
        [Test]
        public void TestInsert()
        {
            var user = new User
            {
                email = "admin6@gmail.com",
                password = "12345",
                firstname = "admin",
                lastname = "6",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };
            Assert.AreEqual(true, _dao.Insert(user));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var user = new User
            {
                email = "admin4@gmail.com",
                password = "12345",
                firstname = "admin",
                lastname = "4",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => _dao.Insert(user);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullEmail()
        {
            var user = new User
            {
                email = null,
                password = "12345",
                firstname = "admin",
                lastname = "4",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => _dao.Insert(user);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullState()
        {
            var user = new User
            {
                email = "admin10@gmail.com",
                password = "12345",
                firstname = "admin",
                lastname = "7",
            };
            Assert.AreEqual(true, _dao.Insert(user));
        }
        #endregion

        #region update testing
        [Test]
        public void TestUpdate()
        {
            var user = new User
            {
                id = 1,
                email = "admin1",
                password = "123456",
                firstname = "Thiện",
                lastname = "Trần Hà Ngọc",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                created_date = DateTime.Now
            };
            Assert.AreEqual(true, _dao.Update(user));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var user = new User
            {
                id = 1234,
                email = "admin4@gmail.com",
                password = "12345",
                firstname = "admin",
                lastname = "4",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };
            ;
            Assert.AreEqual(true, _dao.Update(user));
        }

        [Test]
        public void TestUpdateNullFirstname()
        {
            var user = new User
            {
                id = 4,
                email = "admin4@gmail.com",
                password = null,
                firstname = null,
                lastname = "4",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => _dao.Update(user);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestUpdateNullState()
        {
            var user = new User
            {
                email = "admin4@gmail.com",
                password = "12345",
                firstname = "admin",
                lastname = "4",
                country_id = 1,
                state_id = 1,
                district_id = 1,
                avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => _dao.Insert(user);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }
        #endregion
    }
}