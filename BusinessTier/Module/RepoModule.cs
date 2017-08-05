using BusinessTier.Repository;
using Ninject.Modules;

namespace BusinessTier.Module
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepo>().To<UserRepo>().Named("UserRepo");
            Bind<IRepo>().To<ProjectRepo>().Named("ProjectRepo");
            Bind<IRepo>().To<RoleRepo>().Named("RoleRepo");
            Bind<IRepo>().To<AddressRepo>().Named("AddressRepo");
            Bind<IRepo>().To<FollowingRepo>().Named("FollowingRepo");
        }
    }
}