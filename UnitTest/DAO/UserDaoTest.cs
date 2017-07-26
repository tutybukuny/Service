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
    internal class UserDaoTest
    {
        [SetUp]
        public void SetUp()
        {
            dao = new UserDao(Settings.Default.ConnectionString);
        }

        private UserDao dao;

        [Test]
        public void TestDelete()
        {
            var user = new User {Id = 3};
            Assert.AreEqual(true, dao.Delete(user));
        }

        [Test]
        public void TestDeleteNotId()
        {
            var user = new User {Id = 101212};
            Assert.AreEqual(true, dao.Delete(user));
        }

        [Test]
        public void TestGetAll()
        {
            var users = dao.GetAll();
            var districtDao = new DistrictDao(Settings.Default.ConnectionString);
            var district = districtDao.GetById(1);
            var state = district.State;
            var country = state.Country;
            var expecteUsers = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "admin1@gmail.com",
                    Password = "12345",
                    Firstname = "admin",
                    Lastname = "1",
                    PostalCode = 0,
                    Country = country,
                    State = state,
                    District = district,
                    Avatar = "default"
                },
                new User
                {
                    Id = 2,
                    Email = "admin2@gmail.com",
                    Password = "12345",
                    Firstname = "admin",
                    Lastname = "2",
                    PostalCode = 0,
                    Country = country,
                    State = state,
                    District = district,
                    Avatar = "default"
                }
            };

            var result = true;

            for (var i = 0; i < expecteUsers.Count; i++)
                result = expecteUsers[i].MyEquals(users[i]) && result;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetById()
        {
            var districtDao = new DistrictDao(Settings.Default.ConnectionString);
            var district = districtDao.GetById(1);
            var state = district.State;
            var country = state.Country;
            var expectedUser = new User
            {
                Id = 1,
                Email = "admin1@gmail.com",
                Password = "12345",
                Firstname = "admin",
                Lastname = "1",
                PostalCode = 0,
                Country = country,
                State = state,
                District = district,
                Avatar = "default"
            };

            var user = dao.GetById(1);

            Assert.AreEqual(true, user.MyEquals(expectedUser));
        }

        [Test]
        public void TestGetByNotId()
        {
            var user = dao.GetById(11455);

            Assert.AreEqual(null, user);
        }

        [Test]
        public void TestInsert()
        {
            var user = new User
            {
                Email = "admin5@gmail.com",
                Password = "12345",
                Firstname = "admin",
                Lastname = "5",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = new State {Id = 1},
                District = new District {Id = 1},
                Avatar = "default"
            };
            Assert.AreEqual(true, dao.Insert(user));
        }

        [Test]
        public void TestInsertDuplicateName()
        {
            var user = new User
            {
                Email = "admin4@gmail.com",
                Password = "12345",
                Firstname = "admin",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = new State {Id = 1},
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => dao.Insert(user);
            Assert.That(e, Throws.TypeOf<SqlException>());
        }

        [Test]
        public void TestInsertNullEmail()
        {
            var user = new User
            {
                Email = null,
                Password = "12345",
                Firstname = "admin",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = new State {Id = 1},
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => dao.Insert(user);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestInsertNullState()
        {
            var user = new User
            {
                Email = "admin4@gmail.com",
                Password = "12345",
                Firstname = "admin",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = null,
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => dao.Insert(user);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdate()
        {
            var user = new User
            {
                Id = 4,
                Email = "admin4@gmail.com",
                Password = "12345",
                Firstname = "adminupdate",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = new State {Id = 1},
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            Assert.AreEqual(true, dao.Update(user));
        }

        [Test]
        public void TestUpdateNotId()
        {
            var user = new User
            {
                Id = 1234,
                Email = "admin4@gmail.com",
                Password = "12345",
                Firstname = "admin",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = new State {Id = 1},
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            Assert.AreEqual(true, dao.Update(user));
        }

        [Test]
        public void TestUpdateNullPassword()
        {
            var user = new User
            {
                Id = 4,
                Email = "admin4@gmail.com",
                Password = null,
                Firstname = "admin",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = new State {Id = 1},
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => dao.Update(user);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestUpdateNullState()
        {
            var user = new User
            {
                Email = "admin4@gmail.com",
                Password = "12345",
                Firstname = "admin",
                Lastname = "4",
                PostalCode = 0,
                Country = new Country {Id = 1},
                State = null,
                District = new District {Id = 1},
                Avatar = "default"
            };
            ;
            ActualValueDelegate<object> e = () => dao.Insert(user);
            Assert.That(e, Throws.TypeOf<NullReferenceException>());
        }
    }
}