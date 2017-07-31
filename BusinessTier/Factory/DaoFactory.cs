using DataTier;
using DataTier.Dao;
using DataTier.Module;
using Ninject;

namespace BusinessTier.Factory
{
    public class DaoFactory
    {
        public static IKernel Kernel;

        public static object GetDao(string name)
        {
            if (Kernel == null)
                Kernel = new StandardKernel(new DaoModule());

            switch (name)
            {
                case "CountryDao":
                    return Kernel.Get<IDao<Country>>(name);
                case "StateDao":
                    return Kernel.Get<IDao<State>>(name);
                case "DistrictDao":
                    return Kernel.Get<IDao<District>>(name);
                case "TokenDao":
                    return Kernel.Get<IDao<Token>>(name);
                case "UserDao":
                    return Kernel.Get<IDao<User>>(name);
            }

            return null;
        }
    }
}