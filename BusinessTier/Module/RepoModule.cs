﻿using BusinessTier.Repository;
using Ninject.Modules;

namespace BusinessTier.Module
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepo>().To<UserRepo>().Named("UserRepo");
            Bind<IRepo>().To<ProjectRepo>().Named("ProjectRepo");
        }
    }
}