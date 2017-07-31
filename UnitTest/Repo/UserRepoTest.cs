using BusinessTier.Repository;
using DataTier.Dao;
using Ninject;
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
            kernel.Bind(typeof(IDao<>)).To(typeof(UserDao));
            kernel.Bind<IRepo>().To<UserRepo>();
            _repo = (UserRepo) kernel.Get<IRepo>();
        }

        private UserRepo _repo;

        [Test]
        public void TestCreateRepo()
        {
        }
    }
}