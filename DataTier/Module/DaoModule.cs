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
        }
    }
}