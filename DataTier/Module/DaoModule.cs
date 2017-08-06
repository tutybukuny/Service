using DataTier.Dao;
using Ninject.Modules;

namespace DataTier.Module
{
    public class DaoModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IDao<>)).To(typeof(CountryDao)).Named("CountryDao");
            Bind(typeof(IDao<>)).To(typeof(StateDao)).Named("StateDao");
            Bind(typeof(IDao<>)).To(typeof(DistrictDao)).Named("DistrictDao");
            Bind(typeof(IDao<>)).To(typeof(TokenDao)).Named("TokenDao");
            Bind(typeof(IDao<>)).To(typeof(UserDao)).Named("UserDao");
            Bind(typeof(IDao<>)).To(typeof(ProjectDao)).Named("ProjectDao");
            Bind(typeof(IDao<>)).To(typeof(RoleDao)).Named("RoleDao");
            Bind(typeof(IDao<>)).To(typeof(FollowingDao)).Named("FollowingDao");
            Bind(typeof(IDao<>)).To(typeof(CategoryDao)).Named("CategoryDao");
        }
    }
}