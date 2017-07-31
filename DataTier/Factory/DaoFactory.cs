using DataTier.Dao;
using DataTier.Module;
using Ninject;

namespace DataTier.Factory
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
            }

            return null;
        }
    }
}