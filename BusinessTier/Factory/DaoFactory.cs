using DataTier;
using DataTier.Dao;
using DataTier.Module;
using Ninject;

namespace BusinessTier.Factory
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
            }

            return null;
        }
    }
}