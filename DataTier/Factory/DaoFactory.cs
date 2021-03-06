﻿using DataTier.Dao;
using DataTier.Module;
using Ninject;

namespace DataTier.Factory
{
    public class DaoFactory
    {
        private static IKernel _kernel;

        public static object GetDao(string name)
        {
            if (_kernel == null)
                _kernel = new StandardKernel(new DaoModule());

            switch (name)
            {
                case "CountryDao":
                    return _kernel.Get<IDao<Country>>(name);
                case "StateDao":
                    return _kernel.Get<IDao<State>>(name);
                case "DistrictDao":
                    return _kernel.Get<IDao<District>>(name);
                case "TokenDao":
                    return _kernel.Get<IDao<Token>>(name);
                case "UserDao":
                    return _kernel.Get<IDao<User>>(name);
                case "ProjectDao":
                    return _kernel.Get<IDao<Project>>(name);
                case "RoleDao":
                    return _kernel.Get<IDao<Role>>(name);
                case "FollowingDao":
                    return _kernel.Get<IDao<Following>>(name);
                case "CategoryDao":
                    return _kernel.Get<IDao<Category>>(name);
                case "LikeDao":
                    return _kernel.Get<IDao<Like>>(name);
                case "FollowingProjectDao":
                    return _kernel.Get<IDao<FollowingProject>>(name);
                case "JoinedProjectDao":
                    return _kernel.Get<IDao<JoinedProject>>(name);
            }

            return null;
        }
    }
}