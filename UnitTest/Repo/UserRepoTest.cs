using BusinessTier.Repository;
using DataTier;
using DataTier.Dao;
using Ninject;
using Ninject.Parameters;
using NUnit.Framework;

namespace UnitTest.Repo
{
    [TestFixture]
    internal class UserRepoTest
    {
        [SetUp]
        public void SetUp()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRepo>().To<UserRepo>();
            _repo = (UserRepo) kernel.Get<IRepo>();
        }

        private UserRepo _repo;

        [Test]
        public void TestCreateRepo()
        {
            Assert.AreNotEqual(null, _repo);
        }
    }
}